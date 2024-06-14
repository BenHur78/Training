using Microsoft.Build.Construction;
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello. I am a C# program that reads a Microsoft Visual Studio solution file and outputs to the console the projects within it.");
        
    }

    public static void PrintProjects()
    {
        var solutionFile = SolutionFile.Parse("C:/Dev/Azure/cam2/CAM2.sln");

        // Iterate over each project in the solution
        foreach (var project in solutionFile.ProjectsInOrder)
        {
            Console.WriteLine(project.ProjectName);
        }
    }
}