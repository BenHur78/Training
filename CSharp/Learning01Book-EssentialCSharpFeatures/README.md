# Introduction

In this learning we will follow Pro ASP.NET Core 6 9th edition Adam Freeman book, Chapter 05, Essential C# Features. You can find the book github repository [here.](https://github.com/apress/pro-asp.net-core-6). The book was written in 2022, so the last version of C# in 2022 year was C# 11. This means I think we will see some C# 10 features.

# List of Learning's

## Reducing duplication in using statements
- To reduce duplicated using statements we can use _global using statements_ in a file _GlobalUsing.cs_
```
global using LanguageFeatures.Models;
global using Microsoft.AspNetCore.Mvc;
```
- There are also implicit using statements. ASP.NET core defines global using statements for the commonly required namespaces and that is why we do not need to define using statements for those namespaces in _Program.cs_ file
```
System
System.Collections.Generic
System.IO
System.Linq
System.Net.Http
System.Net.Http.Json
System.Threading
System.Threading.Tasks
Microsoft.AspNetCore.Builder
Microsoft.AspNetCore.Hosting
Microsoft.AspNetCore.Http
Microsoft.AspNetCore.Routing
Microsoft.Extensions.Configuration
Microsoft.Extensions.DependencyInjection
Microsoft.Extensions.Hosting
Microsoft.Extensions.Logging
```


## Managing null values using nullable and non-nullable types

## Mixing static and dynamic values in strings using string interpolation

## Initializing and populate objects using object and collection initializers and target-typed new expressions

## Assigning a value for specific types using pattern matching

## Extending the functionality of a class without modifying it using extension methods

## Expressing functions and methods concisely using lambda expressions

## Defining a variable without explicitly declaring its type using var keyword

## Modifying an interface without requiring changes in ints implementation classes using default implementation

## Performing work asynchronously using tashs and async/await keywords

## Producing a sequence of values over time using an asynchronous enumerable

## Getting the name of a class or member using nameof expression
