# Introduction 
This is chapter 06. We will learn how to unit test asp.net core applications.

## Chapter summary

1. Creating a unit test project
2. Creating an XUnit test
3. Running unit tests
4. Isolating a component for testing


## dotnet commands

- To create the web project
```
dotnet new globaljson --sdk-version 6.0.403 --output Testing/SimpleApp
dotnet new web --no-https --output Testing/SimpleApp --framework net6.0
dotnet new sln -o Testing
dotnet sln Testing add Testing/SimpleApp
```

- To create the unit test project

```
dotnet new xunit -o SimpleApp.Tests --framework net6.0
dotnet sln add SimpleApp.Tests
dotnet add SimpleApp.Tests reference SimpleApp
```

- How to add Mock nuget package

```
dotnet add SimpleApp.Tests package Moq --version 4.16.1
```