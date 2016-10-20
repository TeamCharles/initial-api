# Bangazon Web Initial Site

## Installation

Clone or fork the project.

Set an environment variable to your database:

```Bash
export ASPNETCORE_ENVIRONMENT="Development"
export BangazonWeb_Db_Path="/path/to/bangazon.db"
dotnet ef database update
bower install
dotnet restore && dotnet run
```
