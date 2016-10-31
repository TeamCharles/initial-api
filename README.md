# Bangazon Web Initial Site

## Dependencies

To ensure that the Initial Bangazon App works as intended make sure that you have the following dependencies and technologies on your local machine

1. dotnet 

If you need to download dotnet onto your local machine, visit [Microsoft's Documentation](https://www.microsoft.com/en-us/download/details.aspx?id=30653)

2. bower

If you need to download bower onto your local machine, visit [Bower's Documentation](https://bower.io/)

## Installation

Clone or fork the project. Navigate to where the project is saved on your machine.
Note that if running the application in OSX/UNIX/Windows that the commands below require for you to manually change the path to your database.

### OSX/UNIX

Run the following commands in your terminal. 

```Bash
export ASPNETCORE_ENVIRONMENT="Development"
export BangazonWeb_Db_Path="/path/to/bangazon.db"
dotnet restore
dotnet ef database update
bower install
dotnet run
```

###Windows

Run the following commands in Windows Powershell

```Bash
$env:ASPNETCORE_ENVIRONMENT="Development"
$env:BangazonWeb_Db_Path="/path/to/bangazon.db"
dotnet restore
dotnet ef database update
bower install
dotnet run
```
