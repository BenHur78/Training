# Introduction 
This is chapter 07. We will learn how to create a real application.

## dotnet commands

- To create the web project
```
dotnet new globaljson --sdk-version 6.0.403 --output SportsSln/SportsStore
dotnet new web --no-https --output SportsSln/SportsStore --framework net6.0
dotnet new sln -o SportsSln
dotnet sln SportsSln add SportsSln/SportsStore

```

- To create the unit test

```
dotnet new xunit -o SportsSln/SportsStore.Tests --framework net6.0
dotnet sln SportsSln add SportsSln/SportsStore.Tests
dotnet add SportsSln/SportsStore.Tests reference SportsSln/SportsStore
```

- Adding Moq nuget package
```
dotnet add SportsSln/SportsStore.Tests package Moq --version 4.16.1
```

- To use Microsoft SQL Express using Docker
1. Create a folder to store database files. I have created _sql_ folder in the directory _C:\Dev_
2. Run the following command in a terminal window
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql --mount type=bind,src=C:\Dev\sql,target=/var/opt/mssql/data mcr.microsoft.com/mssql/server:latest
```


- To test a connection string create a file _TestConnectionString.udl_
1. After file creation, double click in the file
2. Select _Connection_ tab
3. On _server name_ type _localhost,1433_ 
4. On _Use a specific user name and password_ type _sa_
5. On _Select the database on the server_ select _master_
6. Click _Test Connection_

![Test connection string image](./images/TestConnectionString.PNG)

- To define a connection string edit _appsettings.json_ file
```
"AllowedHosts": "*",
  "ConnectionStrings": {
    "SportsStoreConnection": "Persist Security Info=False;User ID=sa;Password=MyPass@word;Initial Catalog=SportsStore;Data Source=localhost,1433;MultipleActiveResultSets=true"
  }
```

- Adding Entity Framework Core Packages. You need to execute the command in the _SportsStore_ folder
```
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.0
```

- Installing the Entity Framework Core Tool Packages
```
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef --version 6.0.0
```

- Creating the database migration
```
dotnet ef migrations add Initial
```

- Resetting the database
```
dotnet ef database drop --force --context StoreDbContext
```

- After seeding data to the database

![Products table data image](./images/SeedingDataToDatabase.PNG)


- Installing the Bootstrap Package
```
dotnet tool uninstall --global Microsoft.Web.LibraryManager.Cli
dotnet tool install --global Microsoft.Web.LibraryManager.Cli --version 2.1.113
libman init -p cdnjs
libman install bootstrap@5.2.3 -d wwwroot/lib/bootstrap
```


## Some images of the application

1. Displaying product details

![Products details image](./images/DisplayingProductDetails.PNG)

2. Displaying product details with pagination

![Products details with pagination image](./images/DisplayingProductDetailsWithPagination.PNG)

