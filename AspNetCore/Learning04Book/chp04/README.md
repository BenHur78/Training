# Introduction 
This is chapter 04. We will learn some development tools.

## dotnet commands

- **Get a list of .NET templates**

```
dotnet new --list
```

- **Create the directory structure MySolution/MyProject and then create a .gitignore file**

```
dotnet new gitignore --output MySolution/MyProject
```

- **Creating a new project**

```
dotnet new globaljson --sdk-version 6.0.403 --output MySolution/MyProject
dotnet new web --no-https --output MySolution/MyProject --framework net6.0
dotnet new sln -o MySolution
dotnet sln MySolution add MySolution/MyProject
```