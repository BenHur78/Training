using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // See https://github.com/dotnet/runtime/issues/27515 for more information
            string str = " abc ";
            string sTrimmed = str.Trim();
            Console.WriteLine($"str start={str}=end");
            Console.WriteLine($"str start={sTrimmed}=end");
        }

    }
}