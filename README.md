# Bangazon Web Initial Site

## Dependencies

To ensure that the Initial Bangazon App works as intended make sure that you have the following dependencies and technologies on your local machine

1. dotnet
2. bower

## Installation

Clone or fork the project. Navigate to where the project is saved on your machine.
Please note that if running the application in OSX/UNIX/Windows that the commands below require for you to manually change the path to your database.
In other words, if you copy and paste the following commands they will not initially work until you specifically locate your database file and place the path to said file into the following commands. 

### OSX/UNIX

Please run the following commands in your terminal. 

```Bash
export ASPNETCORE_ENVIRONMENT="Development"
export BangazonWeb_Db_Path="/path/to/bangazon.db"
dotnet restore
dotnet ef database update
bower install
dotnet run
```

###Windows

Please run the following commands in Windows Powershell

```Bash
$env:ASPNETCORE_ENVIRONMENT="Development"
$env:BangazonWeb_Db_Path="/path/to/bangazon.db"
dotnet restore
dotnet ef database update
bower install
dotnet run
```
