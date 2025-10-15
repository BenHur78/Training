// See https://aka.ms/new-console-template for more information


PlayingWithEventWaitHandleVersion7();
//PlayingWithEventWaitHandleVersion6();
//PlayingWithAutoResetEventVersion5();
//PlayingVersion1();
//PlayingVersion2();
//PlayingVersion3();
//PlayingVersion4();


// System.InvalidOperationException Collection was modified; enumeration operation may not execute.
static void PlayingWithEventWaitHandleVersion7()
{
    Console.WriteLine($"{DateTime.Now} Started");

    //It does not automatically reset. Reset means putting the handle in a non signaled state.
    var autoResetEvent1 = new System.Threading.EventWaitHandle(true, EventResetMode.ManualReset); 
    var sharedList = new List<int>();

    var add = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        var r = new Random(100);
        int cyclesLimit = 100;
        int cycle = 0;

        while (cycle < cyclesLimit)
        {
            cycle++;

            var randomInt = r.Next(1, 5);
            System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100)).Wait();

            autoResetEvent1.WaitOne(); //blocks if is not signaled
            sharedList.Add(randomInt); // non signaled, all other threads blocked
            autoResetEvent1.Set(); //set it to signaled, releasing blocked threads
            Console.WriteLine($"{DateTime.Now} Added element");

        }

        Console.WriteLine($"{DateTime.Now} Add Ended");

    }, TaskCreationOptions.LongRunning);

    var remove = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();

        while (add.Status is TaskStatus.Running || (sharedList.Count > 0))
        {
            autoResetEvent1.WaitOne();
            foreach (var element in sharedList) // non signaled, all other threads blocked
            {
                var result = sharedList.GroupBy(i => i);

                foreach (var group in result)
                {
                    Console.WriteLine($"{DateTime.Now} Key = {group.Key} Count = {group.Count()}");
                }
            }

            if (sharedList.Count > 0)
            {
                sharedList.RemoveAt(0);
            }

            autoResetEvent1.Set(); //set it to signaled, releasing blocked threads
        }
    }, TaskCreationOptions.LongRunning);

    System.Threading.Tasks.Task.WaitAll(add, remove);

    Console.WriteLine($"{DateTime.Now} End");

}

static void PlayingWithEventWaitHandleVersion6()
{
    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEvent Started");

    //It automatically reset. Reset means putting the handle in a non signaled state. Similar to AutoResetEvent(true)
    var autoResetEvent1 = new System.Threading.EventWaitHandle(true, EventResetMode.AutoReset);
    var sharedList = new List<int>();

    var add = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        var r = new Random(100);

        while (true)
        {
            var randomInt = r.Next(1, 5);
            System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100)).Wait();

            autoResetEvent1.WaitOne(); //blocks if is not signaled
            sharedList.Add(randomInt); // non signaled, all other threads blocked
            autoResetEvent1.Set(); //set it to signaled, releasing blocked threads
            Console.WriteLine($"{DateTime.Now} Added element");
        }

    }, TaskCreationOptions.LongRunning);

    var remove = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();

        while (true)
        {
            if (sharedList.Count > 0)
            {
                autoResetEvent1.WaitOne();
                foreach (var element in sharedList) // non signaled, all other threads blocked
                {
                    var result = sharedList.GroupBy(i => i);

                    foreach (var group in result)
                    {
                        Console.WriteLine($"{DateTime.Now} Key = {group.Key} Count = {group.Count()}");
                    }
                }

                sharedList.RemoveAt(0);
                autoResetEvent1.Set(); //set it to signaled, releasing blocked threads
            }
        }
    }, TaskCreationOptions.LongRunning);

    System.Threading.Tasks.Task.WaitAll(add, remove);

    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEventEnd");

}

// System.InvalidOperationException Collection was modified; enumeration operation may not execute.
// Fixing exception with an WaitHandle AutoResetEvent.
static void PlayingWithAutoResetEventVersion6()
{
    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEvent Started");
    var autoResetEvent1 = new System.Threading.AutoResetEvent(true);
    var sharedList = new List<int>();

    var add = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        var r = new Random(100);

        while (true)
        {
            var randomInt = r.Next(1, 5);
            System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100)).Wait();

            autoResetEvent1.WaitOne(); //blocks if is not signaled
            sharedList.Add(randomInt); // non signaled, all other threads blocked
            autoResetEvent1.Set(); //set it to signaled, releasing blocked threads
            Console.WriteLine($"{DateTime.Now} Added element");
        }

    }, TaskCreationOptions.LongRunning);

    var remove = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();

        while (true)
        {
            if (sharedList.Count > 0)
            {
                autoResetEvent1.WaitOne();
                foreach (var element in sharedList) // non signaled, all other threads blocked
                {
                    var result = sharedList.GroupBy(i => i);

                    foreach (var group in result)
                    {
                        Console.WriteLine($"{DateTime.Now} Key = {group.Key} Count = {group.Count()}");
                    }
                }

                sharedList.RemoveAt(0);
                autoResetEvent1.Set(); //set it to signaled, releasing blocked threads
            }
        }
    }, TaskCreationOptions.LongRunning);

    System.Threading.Tasks.Task.WaitAll(add, remove);

    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEventEnd");

}

// System.InvalidOperationException Collection was modified; enumeration operation may not execute.
static void PlayingVersion4()
{
    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEvent Started");
    var sharedList = new List<int>();

    var add = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        var r = new Random(100);

        while (true)
        {
            var randomInt = r.Next(1, 5);
            System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100)).Wait();
            sharedList.Add(randomInt);
        }

    }, TaskCreationOptions.LongRunning);

    var remove = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(150)).Wait();

        while (true)
        {
            if (sharedList.Count > 0)
            {
                foreach (var element in sharedList)
                {
                    var result = sharedList.GroupBy(i => i).Count();
                    Console.WriteLine($"{DateTime.Now} Element {element} Count = {result}"); 
                }

                sharedList.RemoveAt(0);
            }
        }
    }, TaskCreationOptions.LongRunning);

    System.Threading.Tasks.Task.WaitAll(add, remove);

    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEventEnd");

}

// Now it waits
static void PlayingVersion3()
{
    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEvent Started");
    
    var add = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(1000)).Wait();
    }, TaskCreationOptions.LongRunning);

    var remove = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(5000)).Wait();

    }, TaskCreationOptions.LongRunning);

    System.Threading.Tasks.Task.WaitAll(add, remove);

    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEventEnd");

}

// It does not wait at all
static void PlayingVersion2()
{
    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEvent Started");
    
    var add = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(1000));
    }, TaskCreationOptions.LongRunning);

    var remove = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(5000));

    }, TaskCreationOptions.LongRunning);

    System.Threading.Tasks.Task.WaitAll(add, remove);

    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEventEnd");

}

static void PlayingVersion1()
{
    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEvent Started");
    var add = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Thread.Sleep(1000);
    }, TaskCreationOptions.LongRunning );
    
    var remove = System.Threading.Tasks.Task.Factory.StartNew(() =>
    {
        System.Threading.Thread.Sleep(5000);

    }, TaskCreationOptions.LongRunning);

    System.Threading.Tasks.Task.WaitAll(add, remove);

    Console.WriteLine($"{DateTime.Now} PlayingWithAutoResetEventEnd");

}

