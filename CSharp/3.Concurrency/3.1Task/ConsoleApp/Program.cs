// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using Task;

Console.WriteLine("Hello, Scott!");

//#1 Iteration
//This will print 1000, 1000 times
//for (int i = 0; i < 1000; i++)
//{
//    ThreadPool.QueueUserWorkItem(delegate
//    {
//        Console.WriteLine(i);
//        Thread.Sleep(1000);
//    });
//}
//Console.ReadLine();

//#2 Iteration
//for (int i = 0; i < 1000; i++)
//{
//    int capturedValue = i; // Create a local variable to capture the current value of i
//    ThreadPool.QueueUserWorkItem(delegate
//    {
//        Console.WriteLine(capturedValue);
//        Thread.Sleep(1000);
//    });
//}
//Console.ReadLine();

//#3 Iteration
//for (int i = 0; i < 1000; i++)
//{
//    int capturedValue = i; // Create a local variable to capture the current value of i
//    MyThreadPool.QueueUserWorkItem(delegate
//    {
//        Console.WriteLine(capturedValue);
//        Thread.Sleep(1000);
//    });
//}
//Console.ReadLine();

//static class MyThreadPool
//{
//    private static readonly BlockingCollection<Action> s_workItems = new BlockingCollection<Action>();

//    public static void QueueUserWorkItem(Action action) => s_workItems.Add(action);

//    static MyThreadPool()
//    {
//        for (int i = 0; i < Environment.ProcessorCount; i++)
//        {
//            //Difference between background and foreground threads.
//            new Thread(() =>
//            {
//                while(true)
//                {
//                    Action action = s_workItems.Take();
//                    action();
//                }

//            }){ IsBackground = true}.Start();
//        }
//    }
//}

//#4 Iteration
//for (int i = 0; i < 1000; i++)
//{
//    int capturedValue = i; // Create a local variable to capture the current value of i
//    MyThreadPool.QueueUserWorkItem(delegate
//    {
//        Console.WriteLine(capturedValue);
//        Thread.Sleep(1000);
//    });
//}
//Console.ReadLine();

//static class MyThreadPool
//{
//    private static readonly BlockingCollection<Action> s_workItems = new BlockingCollection<Action>();

//    public static void QueueUserWorkItem(Action action) => s_workItems.Add(action);

//    static MyThreadPool()
//    {
//        for (int i = 0; i < Environment.ProcessorCount; i++)
//        {
//            //Difference between background and foreground threads.
//            new Thread(() =>
//                {
//                    while (true)
//                    {
//                        Action action = s_workItems.Take();
//                        action();
//                    }

//                })
//                { IsBackground = true }.Start();
//        }
//    }
//}

//#5.1 Iteration - AsyncLocal data magically flows. This happens because ExecutionContext gets the value of AsyncLocal.
//AsyncLocal<int> myValue = new();
//for (int i = 0; i < 1000; i++)
//{
//    myValue.Value = i; // Create a local variable to capture the current value of i
//    ThreadPool.QueueUserWorkItem(delegate
//    {
//        Console.WriteLine(myValue.Value);
//        Thread.Sleep(1000);
//    });
//}
//Console.ReadLine();

//static class MyThreadPool
//{
//    private static readonly BlockingCollection<Action> s_workItems = new BlockingCollection<Action>();

//    public static void QueueUserWorkItem(Action action) => s_workItems.Add(action);

//    static MyThreadPool()
//    {
//        for (int i = 0; i < Environment.ProcessorCount; i++)
//        {
//            //Difference between background and foreground threads.
//            new Thread(() =>
//                {
//                    while (true)
//                    {
//                        Action action = s_workItems.Take();
//                        action();
//                    }

//                })
//            { IsBackground = true }.Start();
//        }
//    }
//}

//#5.2 Iteration - AsyncLocal data does not magically flows.
//It prints 0's.
//AsyncLocal<int> myValue = new();
//for (int i = 0; i < 1000; i++)
//{
//    myValue.Value = i; // Create a local variable to capture the current value of i
//    MyThreadPool.QueueUserWorkItem(delegate
//    {
//        Console.WriteLine(myValue.Value);
//        Thread.Sleep(1000);
//    });
//}
//Console.ReadLine();

//static class MyThreadPool
//{
//    private static readonly BlockingCollection<Action> s_workItems = new BlockingCollection<Action>();

//    public static void QueueUserWorkItem(Action action) => s_workItems.Add(action);

//    static MyThreadPool()
//    {
//        for (int i = 0; i < Environment.ProcessorCount; i++)
//        {
//            //Difference between background and foreground threads.
//            new Thread(() =>
//                {
//                    while (true)
//                    {
//                        Action action = s_workItems.Take();
//                        action();
//                    }

//                })
//                { IsBackground = true }.Start();
//        }
//    }
//}

//#5.3 Iteration - AsyncLocal data does not magically flows.
//It prints 0's.
//AsyncLocal<int> myValue = new();
//for (int i = 0; i < 1000; i++)
//{
//    myValue.Value = i; // Create a local variable to capture the current value of i
//    MyThreadPool.QueueUserWorkItem(delegate
//    {
//        Console.WriteLine(myValue.Value);
//        Thread.Sleep(1000);
//    });
//}
//Console.ReadLine();

//static class MyThreadPool
//{
//    private static readonly BlockingCollection<(Action, ExecutionContext?)> s_workItems = new BlockingCollection<(Action, ExecutionContext?)>();

//    public static void QueueUserWorkItem(Action action) => s_workItems.Add((action, ExecutionContext.Capture()));

//    static MyThreadPool()
//    {
//        for (int i = 0; i < Environment.ProcessorCount; i++)
//        {
//            //Difference between background and foreground threads.
//            new Thread(() =>
//                {
//                    while (true)
//                    {
//                        (Action workItem, ExecutionContext? context) = s_workItems.Take();

//                        if (context is null)
//                        {
//                            workItem();
//                        }
//                        else
//                        {
//                            //ExecutionContext.Run(context, delegate { workItem(); }, null);

//                            //Equivalent way, more efficient, workItem will be passed to the delegate "state => ((Action)state!)()"
//                            //ExecutionContext.Run(context, state => ((Action)state!)(), workItem);
//                            //ExecutionContext.Run(context, (object? state) => ((Action)state!)(), workItem);

//                            //The compiler will emit a warning we are capturing state but the static keyword does not allow it.
//                            //ExecutionContext.Run(context, static (object? state) => workItem(), workItem);
//                        }

//                    }

//                })
//                { IsBackground = true }.Start();
//        }
//    }
//}

//#6.1
//AsyncLocal<int> myValue = new();
//List<MyTask1> tasks = new();
//for (int i = 0; i < 100; i++)
//{
//    myValue.Value = i; // Create a local variable to capture the current value of i
//    tasks.Add(MyTask1.Run(delegate
//    {
//        Console.WriteLine(myValue.Value);
//        Thread.Sleep(1000);
//    }));
//}
//foreach(var t in tasks) t.Wait();

//#6.2
//MyTask1.Delay(2000).ContinueWith(delegate
//{
//    Console.WriteLine("Hello, Maria!");
//});
//Console.ReadLine(); //prevent the program to exit

//#6.3
//MyTask1.Delay(4000).ContinueWith(delegate
//{
//    Console.WriteLine("Hello, Maria!");
//}).Wait();

//#6.4 - it will be nice if we can do this...but this seems arrow code..how we can fix this?
//MyTask1.Delay(2000).ContinueWith(delegate
//{
//    Console.WriteLine("Hello, Maria!");
//    return MyTask1.Delay(2000).ContinueWith(delegate
//    {
//        Console.WriteLine("And, Jon!");
//        return MyTask1.Delay(2000).ContinueWith(delegate
//        {
//            Console.WriteLine("And, Sir!");
//        });
//    });

//}).Wait();

//#6.5 
//MyTask1.Delay(2000).ContinueWith(delegate
//{
//    Console.WriteLine("Hello, Maria!");

//}).ContinueWith(delegate
//{
//    Console.WriteLine("Hello, Jon!");
//}).ContinueWith(delegate
//{
//    Console.WriteLine("And, Sir!");
//}).Wait();

//#6.6 - What I need to do to have this? This will not work and we do not want to use Thread.Sleep.
//for (int i = 0;; i++)
//{
//    MyTask1.Delay(1000);//if I have something called await here....we can use async iterators
//    Console.WriteLine(i);
//}

//#6.7
//foreach(int i in Count(10))
//{
//    Console.WriteLine(i);
//}

//IEnumerable<int> Count(int count)
//{
//    for(int i = 0; i < count; i++)
//    {
//        yield return i;
//    }
//}

//#6.8 - this is not what we want, because there is no wait in MyTask.Delay
//static IEnumerable<MyTask1> PrintAsync()
//{
//    for(int i = 0; i < 10; i++)
//    {
//        yield return MyTask1.Delay(1000); //I want that when task completes, moveNext be called.
//    }
//}

//foreach (var i in PrintAsync())
//{
//    Console.WriteLine("A new yield value...");
//}

//#6.9 - we are getting the delay we wanted
//MyTask1.Iterate(PrintAsync()).Wait();
//static IEnumerable<MyTask1> PrintAsync()
//{
//    for (int i = 0; i < 10; i++)
//    {
//        yield return MyTask1.Delay(1000);//we can improve this by replacing "yield return" with "await"
//        Console.WriteLine(i);
//    }
//}

//#7 - implementing await keyword by implementing an awaiter
//PrintAsync().Wait();

//#8 - An exception happen inside a Task that was not observed
//UnobservedTaskDemo();
ObservedTaskDemo();

static async System.Threading.Tasks.Task PrintAsync()
{
    for (int i = 0; ; i++)
    {
        await MyTask1.Delay(1000);
        Console.WriteLine(i);
    }
}


static void UnobservedTaskDemo()
{
    //Without attaching to the global event handler, the exception will be silently ignored
    TaskScheduler.UnobservedTaskException += (_, e) =>
    {
        if (e.Exception is AggregateException agg)
        {
            foreach (var inner in agg.InnerExceptions)
                Console.WriteLine($"UnobservedTaskException: {inner}\nStackTrace: {inner.StackTrace}");
        }
        else if (e.Exception.InnerException != null)
        {
            var inner = e.Exception.InnerException;
            Console.WriteLine($"UnobservedTaskException: {inner}\nStackTrace: {inner.StackTrace}");
        }
        else
        {
            Console.WriteLine($"UnobservedTaskException: {e.Exception}\nStackTrace: {e.Exception.StackTrace}");
        }
    };

    var task = new TaskDemo.UnobservedTask();
}

static void ObservedTaskDemo()
{
    //Without attaching to the global event handler, the exception will be silently ignored
    TaskScheduler.UnobservedTaskException += (_, e) =>
    {
        if (e.Exception is AggregateException agg)
        {
            foreach (var inner in agg.InnerExceptions)
                Console.WriteLine($"UnobservedTaskException: {inner}\nStackTrace: {inner.StackTrace}");
        }
        else if (e.Exception.InnerException != null)
        {
            var inner = e.Exception.InnerException;
            Console.WriteLine($"UnobservedTaskException: {inner}\nStackTrace: {inner.StackTrace}");
        }
        else
        {
            Console.WriteLine($"UnobservedTaskException: {e.Exception}\nStackTrace: {e.Exception.StackTrace}");
        }
    };

    var task = new TaskDemo.ObservedTask();
}