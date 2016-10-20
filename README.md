# Bangazon Web Initial Site

## Installation

Clone or fork the project. Navigate to where the project is saved on your machine.

Run the following commands to get the project running:

OSX/UNIX
```Bash
export ASPNETCORE_ENVIRONMENT="Development"
export BangazonWeb_Db_Path="/path/to/bangazon.db"
dotnet restore
dotnet ef database update
bower install
dotnet run
```

Windows
```Bash
$env:ASPNETCORE_ENVIRONMENT="Development"
$env:BangazonWeb_Db_Path="/path/to/bangazon.db"
dotnet restore
dotnet ef database update
bower install
dotnet run
```
