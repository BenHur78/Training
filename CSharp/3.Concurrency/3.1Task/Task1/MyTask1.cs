using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Task
{
    /// <summary>
    /// It let us to fork work and to join it.
    ///
    /// Surface area of Task
    /// </summary>
    //public class MyTask
    //{
    //    public bool IsCompleted {
    //        get { }
    //    }

    //    public void SetResult()
    //    {
    //    }

    //    public void SetException(Exception exception)
    //    {
    //    }

    //    public void Wait() {}

    //    /// <summary>
    //    /// When task completes, it will execute the action.
    //    /// </summary>
    //    /// <param name="action"></param>
    //    public void ContinueWith(Action action) {}
        
    //}

    
    public class MyTask1
    {
        private bool _completed;
        private Exception? _exception;
        private Action? _continuation;
        private ExecutionContext? _context;

        public struct Awaiter(MyTask1 t) : INotifyCompletion
        {
            public Awaiter GetAwaiter() => this;

            public bool IsCompleted => t.IsCompleted;

            public void OnCompleted(Action continuation) => t.ContinueWith(continuation);

            public void GetResult() => t.Wait();
        }

        public Awaiter GetAwaiter() => new(this);

        public bool IsCompleted
        {
            get
            {
                //task need to be thread safe. Because something will complete the task, and something can join the task.
                //In a task we can interact with the operation.
                //The real task has code to turn synchronization as cheap as possible
                lock(this)
                {
                    return _completed;
                }
            }
        }

        /// <summary>
        /// The task will complete within timeout amount of time.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static MyTask1 Delay(int timeout)
        {
            MyTask1 t = new();

            //after the timeout happen, complete the task
            new Timer(_ => t.SetResult()).Change(timeout, -1); //the timer will schedule a callback to be executed after the timeout.
            //This does not block the thread.
            //why not use Thread.Sleep()? 

            return t;
        }

        public static MyTask1 Run(Action action)
        {
            MyTask1 t = new ();

            MyThreadPool.QueueUserWorkItem(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    t.SetException(e);
                    return;
                }

                t.SetResult();
            });

            return t;
        }

        public static MyTask1 WhenAll(List<MyTask1> tasks)
        {
            MyTask1 t = new();

            if (tasks.Count == 0)
            {
                t.SetResult();
            }
            else
            {
                int remaining = tasks.Count;
                Action continuation = () =>
                {
                    //We do not know what tasks are doing, they may complete quickly and if do so, 2 different threads may try
                    //to decrement the remaining variable at the same time. So we need to use Interlocked.
                    if (Interlocked.Decrement(ref remaining) == 0)
                    {
                        //TODO: exceptions
                        t.SetResult();
                    }
                };

                foreach (var task in tasks)
                {
                    task.ContinueWith(continuation);
                }
            }

            return t;
        }

        public void SetResult() => Complete(null);

        public void SetException(Exception exception) => Complete(exception);

        public void Wait()
        {
            ManualResetEventSlim? mres = null;

            lock (this)
            {
                if (!_completed)
                {
                    mres = new ManualResetEventSlim(); //by default the initial state is nonsignaled
                    ContinueWith(mres.Set);
                }
            }

            mres?.Wait();

            if (_exception is not null)
            {
                //throw _exception; //not good, this will overwrite the stack trace of _exception.
                //throw new Exception("", _exception);
                ExceptionDispatchInfo.Throw(_exception); //appends the current stack trace instead of overwriting it.
                //throw new AggregateException(_exception);
            }
        }

        /// <summary>
        /// When task completes, it will execute the action.
        /// </summary>
        /// <param name="action"></param>
        public MyTask1 ContinueWith(Action action)
        {
            MyTask1 t = new();

            Action callBack = () =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    t.SetException(e);
                    return;
                }

                t.SetResult();
            };

            lock (this) //In a real implementation we should not lock on "this" because "this" is public and meaning other can lock on it.
            {
                if (_completed)
                {
                    MyThreadPool.QueueUserWorkItem(callBack);
                }
                else
                {
                    _continuation = callBack;
                    _context = ExecutionContext.Capture();
                }
            }

            return t;
        }

        /// <summary>
        /// The action return a Task.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public MyTask1 ContinueWith(Func<MyTask1> action)
        {
            MyTask1 t = new();

            Action callBack = () =>
            {
                try
                {
                    MyTask1 next = action();
                    next.ContinueWith(delegate
                    {
                        if (next._exception is not null)
                        {
                            t.SetException(next._exception);
                        }
                        else
                        {
                            t.SetResult();
                        }
                    });
                }
                catch (Exception e)
                {
                    t.SetException(e);
                    return;
                }

            };

            lock (this) //In a real implementation we should not lock on "this" because "this" is public and meaning other can lock on it.
            {
                if (_completed)
                {
                    MyThreadPool.QueueUserWorkItem(callBack);
                }
                else
                {
                    _continuation = callBack;
                    _context = ExecutionContext.Capture();
                }
            }

            return t;
        }

        /// <summary>
        /// This is what the compiler generates for async/await.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static MyTask1 Iterate(IEnumerable<MyTask1> tasks)
        {
            MyTask1 t = new();

            IEnumerator<MyTask1> e = tasks.GetEnumerator();

            void MoveNext()
            {
                //move the state machine forward, move next, get the task that was returned and when it completes, move forward again.

                try
                {
                    if (e.MoveNext()) //if there is a next task
                    {
                        MyTask1 next = e.Current; //what was yielded
                        next.ContinueWith(MoveNext);
                        return;
                    }

                }
                catch (Exception e)
                {
                    t.SetException(e);
                    return;
                }

                t.SetResult();

            }

            MoveNext(); //kick off the state machine

            return t;
        }

        /// <summary>
        /// The operation was completed.
        /// </summary>
        /// <param name="exception"></param>
        private void Complete(Exception? exception)
        {
            lock(this)
            {
                if(_completed)
                {
                    throw new InvalidOperationException("Stop messing up my code");
                }

                _completed = true;
                _exception = exception;

                if (_continuation is not null)
                {
                    MyThreadPool.QueueUserWorkItem(delegate
                    {

                        if (_context is null)
                        {
                            _continuation();
                        }
                        else
                        {
                            //_continuation will be passed to the delegate/callback "state => ((Action)state!)()".
                            ExecutionContext.Run(_context, (object? state) => ((Action)state!)(), _continuation);
                        }

                    });
                }
            }

        }

    }
}

public static class MyThreadPool
{
    private static readonly BlockingCollection<(Action, ExecutionContext?)> s_workItems = new BlockingCollection<(Action, ExecutionContext?)>();

    public static void QueueUserWorkItem(Action action) => s_workItems.Add((action, ExecutionContext.Capture()));

    static MyThreadPool()
    {
        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            //Difference between background and foreground threads.
            new Thread(() =>
                {
                    while (true)
                    {
                        (Action workItem, ExecutionContext? context) = s_workItems.Take();

                        if (context is null)
                        {
                            workItem();
                        }
                        else
                        {
                            ExecutionContext.Run(context, (object? state) => workItem(), workItem);
                        }

                    }

                })
            { IsBackground = true }.Start();
        }
    }
}
