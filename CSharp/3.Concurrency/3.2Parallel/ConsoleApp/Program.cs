// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

Parallel.For(0, 10, i => A(i));

[MethodImpl(MethodImplOptions.NoInlining)]
static void A(int i) => B(i);

[MethodImpl(MethodImplOptions.NoInlining)]
static void B(int i) => C(i);

[MethodImpl(MethodImplOptions.NoInlining)]
static void C(int index)
{
    ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxIoThreads);
    ThreadPool.GetMinThreads(out int minWorkerThreads, out int minIoThreads);
    ThreadPool.GetAvailableThreads(out int availWorkerThreads, out int availIoThreads);
    int activeWorkerThreads = maxWorkerThreads - availWorkerThreads;
    int activeIoWorkerThreads = maxWorkerThreads - availIoThreads;
    Console.WriteLine($"Index {index} - activeWorkerThreads = {activeWorkerThreads} availWorkerThreads = {availWorkerThreads} activeIoWorkerThreads = {activeIoWorkerThreads} availIoThreads = {availIoThreads} maxWorkerThreads = {maxWorkerThreads} maxIoThreads = {maxIoThreads} ");

    Thread.Sleep(1_000_000);
}


