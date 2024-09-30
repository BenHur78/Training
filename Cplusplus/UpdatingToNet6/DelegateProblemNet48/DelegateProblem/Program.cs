using System;
using MeshingWrapper;

namespace MyNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var result = PcIOManager.Load(@"c:\xpto.txt", null);

            // Your code here

            Console.ReadLine();
        }
    }
}