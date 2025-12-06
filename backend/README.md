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
2. Start a postgresql server by running `sudo service postgresql start`.
- Note the port number
3. Add a PostgreSQL connection string to the new `appsettings.Development.json` file
```json
  "ConnectionStrings": {
    "PgSQLConnection": "Server=localhost;Port={port number};Database=bmsDB;User Id={user id};Password={password};"
  }
```
4. Run the following commands to create the database:
```bash
dotnet ef database drop
dotnet ef database update
```
5. Run the following command to start the server:
```bash
dotnet run --launch-profile https
```

Swagger documentation URL: `https://localhost:7090/swagger`

## Database
Basic commands:
- `sudo service postgresql {start/restart/stop}`
- `sudo -u postgres {createdb/dropdb} {db name}`
- `psql -U postgres`

[Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

## Other Commands
To generate a controller file:
```bash
dotnet aspnet-codegenerator controller -name {Module}sController -async -api -m {Module} -dc {Module}Context -outDir Controllers
```
Example:
```bash
dotnet aspnet-codegenerator controller -name BuildingsController -async -api -m Building -dc BuildingContext -outDir Controllers
```