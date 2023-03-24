# Introduction 
This is chapter 02. We will configure Microsoft SQL Server express edition in a local machine (eg windows 10 machine).

## dotnet commands

- **How to list the installed Dot.Net SDK's in your local machine**

```
dotnet --list-sdks
```

- **How to list the installed Dot.Net SDK's in your local machine**

```
dotnet new globaljson --sdk-version 6.0.403 --output FirstProject
dotnet new mvc --no-https --output FirstProject --framework net6.0
dotnet new sln -o FirstProject
dotnet sln FirstProject add FirstProject
```

- **dotnet new globaljson** this command creates a folder named FirstProject and adds to it a file named global.json, which specifies the version of .NET that the project will use; this ensures you get the expected results when following the examples.


## How to use Microsoft SQL Express using Docker

- **How to create a docker container**

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql mcr.microsoft.com/mssql/server:latest
```

- _docker run [OPTIONS] IMAGE[:TAG|@DIGEST] [COMMAND] [ARG...]_
- **-e** defines an environment variable
- **-e "MSSQL_PID=Express"** using Microsoft SQL Server express edition.
- **--name=sql** the docker container name is _sql_
- **-p 1433:1433** this means the _1433_ port will be available in the host machine (eg your windows machine).
- **-d** run the docker container in detached (aka background) mode
- You can see additional details related to docker run [here.](https://docs.docker.com/engine/reference/run/)

- **How to stop, start or remove a docker container**

```
docker start sql
docker stop sql
docker rm -f sql
```

- **docker start sql** start the docker container
- **docker stop sql** stop the docker container
- **docker rm -f sql** force the removal of a running container (uses SIGKILL)

- **How to map a host folder to docker container folder**

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql --mount type=bind,src=C:\Dev\GitHubAlberto\Training\AspNetCore\Learning04Book\sql,target=/var/opt/mssql/data mcr.microsoft.com/mssql/server:latest
```

- **--mount** Consists of multiple key-value pairs, separated by commas and each consisting of a <key>=<value> tuple
- **type=bind**
- **src=C:\Dev...** a path to the host directory
- **target=C:\Dev...** a path to target folder (target is also known as destination)
- You can see additional details related to mount option [here.](https://docs.docker.com/storage/bind-mounts/)

- **How to map a host folder to docker container folder using volumes (this is better than using mount)**
To use print working directory command _pwd_ we assume you will run the command in the directory you want to store sql data. We also assume you are using a terminal window.


```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql -v ${pwd}:/var/opt/mssql/data mcr.microsoft.com/mssql/server:latest
```

- **-v ${pwd}:/var/opt/mssql/data**
- **\${pwd}** if this does not work try with _$(pwd)_ or _%cd%_ if you are using a windows command line (cmd).
- There is another option using named volumes _sqlData_ like the command below

```
docker run --user root -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql -v -v sqlData:/var/opt/mssql/data mcr.microsoft.com/mssql/server:latest
```

- **--user root** You may need to run the docker container as root. In other words, Microsoft SQL Server need to run as root. If not, you may have this error. To understand why, please consult this [article.](https://stackoverflow.com/questions/65601077/unable-to-run-sql-server-2019-docker-with-volumes-and-get-error-setup-failed-co)

```
ERROR: Setup FAILED copying system data file 'C:\templatedata\master.mdf' to '/var/opt/mssql/data/master.mdf':  5(Access is denied.)
```



