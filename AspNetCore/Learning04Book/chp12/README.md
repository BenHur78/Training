# Introduction 
We will learn how to create a real application, following chapter's from 07 to 11.

* Chapter 07 - SportsStore: A real application
* Chapter 08 - SportsStore: Navigation and Cart
* Chapter 09 - SportsStore: Completing the Cart
* Chapter 10 - SportsStore: Administration
* Chapter 11 - SportsStore: Security and Deployment

## dotnet commands

1. To create the web project
```
dotnet new globaljson --sdk-version 6.0.403 --output Platform
dotnet new web --no-https --output Platform --framework net6.0
dotnet new sln -o Platform
dotnet sln Platform add Platform
```

2. To run the example application
```
dotnet run
```

3. How to send an Http Request
1. Open Powershell and run this command
```
(Invoke-WebRequest http://localhost:5000).RawContent
```
2. You will see the response with http headers.


4. Adding a Package to the Project
```
dotnet add package Swashbuckle.AspNetCore --version 6.2.2
```
