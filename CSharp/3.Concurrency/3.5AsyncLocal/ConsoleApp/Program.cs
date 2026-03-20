using System;
using System.Threading;
using System.Threading.Tasks;

class Example
{
    static AsyncLocal<string> _asyncLocalString = new AsyncLocal<string>(PrintValues);

    static ThreadLocal<string> _threadLocalString = new ThreadLocal<string>();

    static void PrintValues(AsyncLocalValueChangedArgs<string> args)
    {
        Console.WriteLine($"This is an notifcation. PreviousValue is '{args.PreviousValue}', CurrentValue is '{args.CurrentValue}'. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
    }

    static async Task AsyncMethodA()
    {
        // Start multiple async method calls, with different AsyncLocal values.
        // We also set ThreadLocal values, to demonstrate how the two mechanisms differ.
        _asyncLocalString.Value = "Value 1";
        _threadLocalString.Value = "Value 1";
        var t1 = AsyncMethodB("Value 1");

        _asyncLocalString.Value = "Value 2";
        _threadLocalString.Value = "Value 2";
        var t2 = AsyncMethodB("Value 2");

        // Await both calls
        await t1;
        await t2;
     }

    static async Task AsyncMethodB(string expectedValue)
    {
        Console.WriteLine($"Entering AsyncMethodB where expected value is '{expectedValue}'. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine($"   Expected '{expectedValue}', AsyncLocal value is '{_asyncLocalString.Value}', ThreadLocal value is '{_threadLocalString.Value}'. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(100);
        Console.WriteLine($"Exiting AsyncMethodB where expected value is '{expectedValue}'. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine($"   Expected '{expectedValue}', got '{_asyncLocalString.Value}', ThreadLocal value is '{_threadLocalString.Value}'. Thread Id: {Thread.CurrentThread.ManagedThreadId}");    
    }

    static async Task Main(string[] args)
    {
        await AsyncMethodA();
    }
}

//Entering AsyncMethodB where expected value is 'Value 1'. Thread Id: 1
//   Expected 'Value 1', AsyncLocal value is 'Value 1', ThreadLocal value is 'Value 1'. Thread Id: 1
//Entering AsyncMethodB where expected value is 'Value 2'. Thread Id: 1
//   Expected 'Value 2', AsyncLocal value is 'Value 2', ThreadLocal value is 'Value 2'. Thread Id: 1
//Exiting AsyncMethodB where expected value is 'Value 2'. Thread Id: 8
//   Expected 'Value 2', got 'Value 2', ThreadLocal value is ''. Thread Id: 8
//Exiting AsyncMethodB where expected value is 'Value 1'. Thread Id: 6
//   Expected 'Value 1', got 'Value 1', ThreadLocal value is ''. Thread Id: 6

// With notifications:
// This is an notifcation. PreviousValue is '', CurrentValue is 'Value 1'. Thread Id: 1
// Entering AsyncMethodB where expected value is 'Value 1'. Thread Id: 1
//    Expected 'Value 1', AsyncLocal value is 'Value 1', ThreadLocal value is 'Value 1'. Thread Id: 1
// This is an notifcation. PreviousValue is 'Value 1', CurrentValue is 'Value 2'. Thread Id: 1
// Entering AsyncMethodB where expected value is 'Value 2'. Thread Id: 1
//    Expected 'Value 2', AsyncLocal value is 'Value 2', ThreadLocal value is 'Value 2'. Thread Id: 1
// This is an notifcation. PreviousValue is 'Value 2', CurrentValue is ''. Thread Id: 1
// This is an notifcation. PreviousValue is '', CurrentValue is 'Value 2'. Thread Id: 10
// This is an notifcation. PreviousValue is '', CurrentValue is 'Value 1'. Thread Id: 6
// Exiting AsyncMethodB where expected value is 'Value 1'. Thread Id: 6
// Exiting AsyncMethodB where expected value is 'Value 2'. Thread Id: 10
//    Expected 'Value 1', got 'Value 1', ThreadLocal value is ''. Thread Id: 6
//    Expected 'Value 2', got 'Value 2', ThreadLocal value is ''. Thread Id: 10
// This is an notifcation. PreviousValue is 'Value 2', CurrentValue is ''. Thread Id: 10
// This is an notifcation. PreviousValue is 'Value 1', CurrentValue is 'Value 2'. Thread Id: 6
// This is an notifcation. PreviousValue is 'Value 2', CurrentValue is ''. Thread Id: 6
// This is an notifcation. PreviousValue is '', CurrentValue is 'Value 2'. Thread Id: 6
// This is an notifcation. PreviousValue is 'Value 2', CurrentValue is 'Value 1'. Thread Id: 6
// This is an notifcation. PreviousValue is 'Value 1', CurrentValue is ''. Thread Id: 6