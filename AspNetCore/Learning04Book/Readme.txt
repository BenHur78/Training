***
Commits
***

Learning04Book - chapter 06 - 

***
Using dotnet command
***

1.1 This command creates a folder named FirstProject and adds to it a file named global.json, which specifies the version of .NET that 
the project will use; this ensures you get the expected results when following the examples.

dotnet new globaljson --sdk-version 6.0.403 --output FirstProject

dotnet new mvc --no-https --output FirstProject --framework net6.0

dotnet new sln -o FirstProject

dotnet sln FirstProject add FirstProject

1.2 How to create a .gitignore file

dotnew gitignore --output MySolution/MyProject

2. This sets Hot reload. In Ms Visual Studio open a Developer Command prompt and run this command. When you do a change in a file, it will
automatically compile and build and run the application.

dotnet watch

3. Managing Packages

3.1 Adding a Package to the Example Project

dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.0

3.2 How to list the packages in a project

dotnet list package

3.3 To remove a package
dotnet remove package Microsoft.EntityFrameworkCore.SqlServer

3.4 Installing tool package's
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef --version 6.0.0

3.4.1 Installing client side packages
dotnet tool uninstall --global Microsoft.Web.LibraryManager.Cli
dotnet tool install --global Microsoft.Web.LibraryManager.Cli --version 2.1.113

4. Create a unit test project

dotnet new xunit -o SimpleApp.Tests --framework net6.0

dotnet sln add SimpleApp.Tests

dotnet add SimpleApp.Tests reference SimpleApp

5. Templates
5.1 web - ASP.Net Core Empty
5.2 mvc - ASP.NET Core Web App (Model-View-Controller)
5.3 nunit - creates a project configured for NUnit framework
5.4 xunit - creates a project configured for XUnit framework
5.5 mstest - creates a project configured for MS Test framework, which is produced by Microsoft

***
Using libman command
***

1. Set LibraryManager (LibMan) to download packages from https://cdnjs.com repository
libman init -p cdnjs


2. Installing the Bootstrap CSS Framework
libman install bootstrap@5.2.3 -d wwwroot/lib/bootstrap

***
Using Docker to run a local SQL Server Express
***
1. Open terminal window
1.1 Run this command

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql mcr.microsoft.com/mssql/server:latest

1.2 This is the connection string

Server=localhost,1433;Initial Catalog=MyDb;Integrated Security=True;User Id=sa;Password=MyPass@word;

1.3 To change the port from 1433 to 1439:

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1439:1433 -d --name=sql mcr.microsoft.com/mssql/server:latest

1.4 Commands to remove the container or to stop or start the container

//remove the container
docker rm -f sql

docker start sql

docker stop sql

1.5 To keep sql data we need to mount a volume
1.5.1 Let assume your working directory is "C:\Dev\GitHubAlberto\Training\AspNetCore\Learning04Book"
1.5.2 Create a directory named "sql"
1.5.3 Stop and remove sql container if it exist. Run the following command to create a new container that is mouted to your local volume:

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql --mount type=bind,src=C:\Dev\GitHubAlberto\Training\AspNetCore\Learning04Book\sql,target=/var/opt/mssql/data mcr.microsoft.com/mssql/server:latest

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql --v sqlData:/var/opt/mssql/data mcr.microsoft.com/mssql/server:latest

***
Other commands
***
1.1 How to kill a process in windows? This sometimes happen when we start an asp.net core application in VS Code and it says that some files
are locked by another process.

taskkill /pid 78032 /f
