using ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var serviceProvider = new ServiceCollection()
    .AddLogging(builder =>
    {
        builder.SetMinimumLevel(LogLevel.Trace);
        //builder.ClearProviders();
        //builder.AddConsole();
        //builder.AddDebug();
        builder.AddProvider(new FileLoggerProvider(@"C:\Dev\Alberto\Training\AspNetCore\Learning05"));
    })
    .BuildServiceProvider();

// This works
//ServiceProvider serviceProvider = new ServiceCollection()
//    .AddLogging((loggingBuilder) => loggingBuilder
//        .SetMinimumLevel(LogLevel.Trace)
//        .AddConsole()
//    )
//    .BuildServiceProvider();

// I've tried this
var logger = serviceProvider.GetService<ILogger<Program>>();

// And this
//var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

logger.LogDebug("Debug world");       // <-- This DOESN'T work
logger.LogInformation("Information world");   // <-- This DOES work
logger.LogTrace("Trace world");