# Bangazon Web Initial Site

## Dependencies

To ensure that the Initial Bangazon App works as intended make sure that you have the following dependencies and technologies on your local machine

- dotnet 

If you need to download dotnet onto your local machine, visit [Microsoft's Documentation](https://www.microsoft.com/en-us/download/details.aspx?id=30653)

- bower

If you need to download bower onto your local machine, visit [Bower's Documentation](https://bower.io/)

## Installation OSX/UNIX

Clone or fork the project. Navigate to where the project is saved on your machine. For all of the following commands enter them into your bash terminal to ensure that the application is installed correctly


This command sets the environment for your local copy of the application to development mode.
```Bash
export ASPNETCORE_ENVIRONMENT="Development"
```

On initial installation of the Banagazon web application you must set an environment variable to your local database. (This database with the file name bangazon.db will be created when you run dotnet ef database update later in the installation process.)
```Bash
export Bangazon_Order_DB="/path/to/bangazon.db"
```

Once your local variables have been set run the following commands to start. `dotnet ef database update` will create the database for your application at the location that was set earlier.
```Bash
dotnet restore
dotnet ef database update
bower install
dotnet run
```

## Installation Windows

Clone or fork the project. Navigate to where the project is saved on your machine. For all of the following commands enter them into your bash terminal to ensure that the application is installed correctly


This command sets the environment for your local copy of the application to development mode.
```Bash
$env:ASPNETCORE_ENVIRONMENT="Development"
```

On initial installation of the Banagazon web application you must set an environment variable to your local database. Even though you have not yet created a database, this variable will set the location of your database for later after creation.
```Bash
$env:Bangazon_Order_DB="/path/to/bangazon.db"
```

Once your local variables have been set run the following commands to start. `dotnet ef database update` will create the database for your application at the location that was set earlier.
```Bash
dotnet restore
dotnet ef database update
bower install
dotnet run
```
