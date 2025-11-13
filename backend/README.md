# building-management-system Backend

A cloud-based building management system designed to streamline 
daily operations and tasks for property managers and building 
owners by centralising work order tracking, contractor management
and building management in one platform.


## Technology Stack/Documentation
### Language
  - [C#](https://learn.microsoft.com/en-us/dotnet/csharp/?WT.mc_id=dotnet-35129-website)
  - [.NET](https://learn.microsoft.com/en-us/dotnet/?WT.mc_id=dotnet-35129-website)
### Framework
  - [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-9.0&WT.mc_id=dotnet-35129-website)


## Quickstart
To run development server
1. Copy `appsettings.json` into a new `appsettings.Development.json` file
2. Start a postgresql server on port *5432* and create a database
3. Add a PostgreSQL connection string named *PgSQLConnection*
4. Run the following command:
```bash
dotnet run --launch-profile https
```

Swagger documentation URL: `https://localhost:7090/swagger`

## Database
Useful commands:
- `sudo service postgresql {start/restart/stop}`
- `sudo -u postgres {createdb/dropdb} {db name}`
- `psql -U postgres`

## Other Commands
To generate a controller file:
```bash
dotnet aspnet-codegenerator controller -name {Module}sController -async -api -m {Module} -dc {Module}Context -outDir Controllers
```
Example:
```bash
dotnet aspnet-codegenerator controller -name BuildingsController -async -api -m Building -dc BuildingContext -outDir Controllers
```