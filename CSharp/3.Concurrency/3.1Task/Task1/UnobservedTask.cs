namespace TaskDemo
{
    using System.Threading.Tasks;

    public class UnobservedTask
    {
        public UnobservedTask()
        {
            Task.Factory.StartNew(() => throw new InvalidOperationException("Something went wrong!"));
            
            Thread.Sleep(1000); // Program ends, Task gets GC'd

            //If you do not wait on a task that propagates an exception, or access its Exception property,
            //the exception is escalated according to the .NET exception policy when the task is garbage-collected.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        
    }

    public class ObservedTask
    {
        public ObservedTask()
        {
            var task = Task.Factory.StartNew(() => throw new InvalidOperationException("Something went wrong!"));

            Thread.Sleep(1000); // Program ends, Task gets GC'd

            //if (task.IsCompleted)
            //{
            //    var e = task.Exception;
            //}

            //If you do not wait on a task that propagates an exception, or access its Exception property,
            //the exception is escalated according to the .NET exception policy when the task is garbage-collected.
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

    }
}
