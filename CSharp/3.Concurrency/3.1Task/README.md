# Introduction 
The purpose is to study this amazing youtube video https://www.youtube.com/watch?v=R-z2Hv-7nxk, Deep .NET: Writing async/await from scratch in C# with Stephen Toub and Scott Hanselman.

# What we learned here
1. Asynchronous versus Concurrency, are different things. Concurrency is doing multiple things at the same time. Doing things asynchronously that is not quite the same, is "firing and forget" something and this does not mean it will going run concurrently, let say, this is an UI application, it will run concurrently with the UI thread. We cannot have concurrency without asynchronicy but we can have asynchronicy without concurrency. 
2. The important concept here is that QueueUserWorkItem captures the execution context and the execution context does not capture the value, it captures the reference, in this case the “I” variable reference. When the work item executes, the value was changed. 
3. Difference between background threads and foreground threads. The difference between them is when your main function exit, do you want that all the threads exit as well? For Foreground threads, operating system will wait, for background it will not wait.