using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var serviceProvider = new ServiceCollection()
    .AddLogging(builder => {
        builder.ClearProviders();
        builder.AddConsole();
        builder.AddDebug();
    })
    .BuildServiceProvider();

// I've tried this
//var logger = serviceProvider.GetService<ILogger<Program>>();

// And this
var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

logger.LogDebug("hello world");       // <-- This DOESN'T work
logger.LogInformation("something");   // <-- This DOES work