using Microsoft.Build.Construction;
using QuickGraph;
using QuickGraph.Algorithms.ConnectedComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static Dictionary<string, ProjectInfo> projects = new Dictionary<string, ProjectInfo>();

    static void Main()
    {
        var directory = "C:/Dev/Azure/cam2/";
        //var directory = "C:/Dev/Azure/faro.devicecenter/";
        Console.WriteLine($"Start scan .csproj files on {directory} directory.");

        // Scan the directory and find .csproj files
        ScanDirectory(directory);

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. List all project names alphabetically.");
            Console.WriteLine("2. Get dependencies of a specific project.");
            Console.WriteLine("3. List projects with target framework different from net48.");
            Console.WriteLine("4. Get the target framework of a specific project.");
            Console.WriteLine("5. List all project names alphabetically of projects that are in net48.");
            Console.WriteLine("6. Display project dependency matrix.");
            Console.WriteLine("7. Identify clusters of projects that do not depend on each other.");
            Console.WriteLine("8. List projects that reference specific assemblies.");
            Console.WriteLine("9. List projects that depend on a specific project.");
            Console.WriteLine("10. Exit.");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListProjectNames();
                    break;
                case "2":
                    GetProjectDependencies();
                    break;
                case "3":
                    ListDifferentTargetFrameworkProjects();
                    break;
                case "4":
                    GetProjectFramework();
                    return;
                case "5":
                    ListProjectsWithTargetFrameworkEqualToNet48();
                    break;
                case "6":
                    SaveDependencyMatrixToFile(directory);
                    break;
                case "7":
                    IdentifyProjectClusters();
                    break;
                case "8":
                    ListProjectsWithSpecificReferences();
                    break;
                case "9":
                    ListDependentProjects();
                    break;
                case "10":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void ScanDirectory(string directory)
    {
        foreach (string file in Directory.GetFiles(directory, "*.csproj", SearchOption.AllDirectories))
        {
            XDocument doc = XDocument.Load(file);
            XElement projectElement = doc.Element("Project");

            if (projectElement.Attribute("Sdk")?.Value == "Microsoft.NET.Sdk" 
                || projectElement.Attribute("Sdk")?.Value == "Microsoft.NET.Sdk.WindowsDesktop")
            {
                string projectName = Path.GetFileNameWithoutExtension(file);
                var dependencies = projectElement
                    .Elements("ItemGroup")
                    .Elements("ProjectReference")
                    .Select(e => Path.GetFileNameWithoutExtension(e.Attribute("Include").Value))
                    .ToList();

                string targetFramework = projectElement
                    .Elements("PropertyGroup")
                    .Elements("TargetFramework")
                    .FirstOrDefault()?.Value;

                var references = projectElement
                    .Elements("ItemGroup")
                    .Elements("Reference")
                    .Select(e => e.Attribute("Include")?.Value)
                    .Where(v => v != null)
                    .ToList();

                projects[projectName] = new ProjectInfo
                {
                    Path = file,
                    Dependencies = dependencies,
                    TargetFramework = targetFramework,
                    References = references
                };
            }
        }
    }

    static void ListProjectNames()
    {
        var projectNames = projects.Keys.OrderBy(name => name).ToList();
        Console.WriteLine("Project names (alphabetically):");
        projectNames.ForEach(name => Console.WriteLine(name));
    }

    static void GetProjectDependencies()
    {
        Console.WriteLine("Enter the project name:");
        string projectName = Console.ReadLine();

        if (projects.TryGetValue(projectName, out ProjectInfo projectInfo))
        {
            Console.WriteLine($"Dependencies of {projectName}:");
            projectInfo.Dependencies.ForEach(dep => Console.WriteLine(dep));
        }
        else
        {
            Console.WriteLine("Project not found.");
        }
    }

    static void ListDifferentTargetFrameworkProjects()
    {
        var differentFrameworkProjects = projects
            .Where(p => p.Value.TargetFramework != "net48")
            .Select(p => p.Key)
            .OrderBy(name => name)
            .ToList();

        Console.WriteLine("Projects with target framework different from net48 (alphabetically):");
        differentFrameworkProjects.ForEach(name => Console.WriteLine(name));
    }

    static void ListProjectsWithTargetFrameworkEqualToNet48()
    {
        var differentFrameworkProjects = projects
            .Where(p => p.Value.TargetFramework == "net48")
            .Select(p => p.Key)
            .OrderBy(name => name)
            .ToList();

        Console.WriteLine("Projects with target framework equal to net48 (alphabetically):");
        differentFrameworkProjects.ForEach(name => Console.WriteLine(name));
    }

    static void GetProjectFramework()
    {
        Console.WriteLine("Enter the project name:");
        string projectName = Console.ReadLine();

        if (projects.TryGetValue(projectName, out ProjectInfo projectInfo))
        {
            Console.WriteLine($"The target framework of {projectName} is {projectInfo.TargetFramework}.");
        }
        else
        {
            Console.WriteLine("Project not found.");
        }
    }

    static void SaveDependencyMatrixToFile(string directory)
    {
        var projectNames = projects.Keys.OrderBy(name => name).ToList();
        int n = projectNames.Count;
        int[,] matrix = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            string project = projectNames[i];
            foreach (string dependency in projects[project].Dependencies)
            {
                int j = projectNames.IndexOf(dependency);
                if (j != -1)
                {
                    matrix[i, j] = 1;
                }
            }
        }

        string filePath = $"{directory}dependency_matrix.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    writer.Write(matrix[i, j] == 1 ? "1" : "");
                    if (j < n - 1)
                    {
                        writer.Write("\t");
                    }
                }
                writer.WriteLine();
            }
        }

        Console.WriteLine($"Dependency matrix saved to {filePath}");
    }

    static void IdentifyProjectClusters()
    {
        var projectNames = projects.Keys.ToList();
        var graph = new AdjacencyGraph<string, Edge<string>>();

        foreach (var project in projectNames)
        {
            graph.AddVertex(project);
        }

        foreach (var project in projects)
        {
            foreach (var dependency in project.Value.Dependencies)
            {
                if (projects.ContainsKey(dependency))
                {
                    graph.AddEdge(new Edge<string>(project.Key, dependency));
                }
            }
        }

        var algorithm = new WeaklyConnectedComponentsAlgorithm<string, Edge<string>>(graph);
        algorithm.Compute();

        var clusters = algorithm.Components
            .GroupBy(kvp => kvp.Value)
            .Select(g => g.Select(kvp => kvp.Key).ToList())
            .ToList();

        Console.WriteLine("Identified clusters of projects that do not depend on each other:");
        for (int i = 0; i < clusters.Count; i++)
        {
            Console.WriteLine($"Cluster {i + 1}: {string.Join(", ", clusters[i])}");
        }
    }

    static void ListProjectsWithSpecificReferences()
    {
        var specificReferences = new HashSet<string> { "PresentationCore", "PresentationFramework", "System.Xaml", "WindowsBase" };
        var projectsWithSpecificReferences = projects
            .Where(p => p.Value.References.Any(r => specificReferences.Contains(r)))
            .Select(p => p.Key)
            .OrderBy(name => name)
            .ToList();

        Console.WriteLine("Projects that reference PresentationCore, PresentationFramework, System.Xaml, or WindowsBase:");
        projectsWithSpecificReferences.ForEach(name => Console.WriteLine(name));
    }

    static void ListDependentProjects()
    {
        Console.WriteLine("Enter the project name:");
        string projectName = Console.ReadLine();

        if (!projects.ContainsKey(projectName))
        {
            Console.WriteLine("Project not found.");
            return;
        }

        var dependentProjects = projects
            .Where(p => p.Value.Dependencies.Contains(projectName))
            .Select(p => p.Key)
            .OrderBy(name => name)
            .ToList();

        if (dependentProjects.Any())
        {
            Console.WriteLine($"Projects that depend on {projectName}:");
            dependentProjects.ForEach(name => Console.WriteLine(name));
        }
        else
        {
            Console.WriteLine($"No projects depend on {projectName}.");
        }
    }

    #region Archive

    public static void Archived_PrintProjects()
    {
        Console.WriteLine("Hello. I am a C# program that reads a Microsoft Visual Studio solution file and outputs to the console the projects within it.");

        var solutionFile = SolutionFile.Parse("C:/Dev/Azure/cam2/CAM2.sln");

        // Iterate over each project in the solution
        foreach (var project in solutionFile.ProjectsInOrder)
        {
            Console.WriteLine(project.ProjectName);
        }
    }

    #endregion

}

class ProjectInfo
{
    public string Path { get; set; }
    public List<string> Dependencies { get; set; }
    public string TargetFramework { get; set; }
    public List<string> References { get; set; }
}