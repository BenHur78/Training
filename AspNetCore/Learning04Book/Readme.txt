***
Commits
***

Learning04Book - chapter 04 - 

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
