# Building Management System - Backend

A cloud-based building management system designed to streamline 
daily operations and tasks for property managers and building 
owners. 
It centralises work order tracking, contractor management and 
building management in one platform.


## Technology Stack
### Language
  - [C#](https://learn.microsoft.com/en-us/dotnet/csharp/?WT.mc_id=dotnet-35129-website)
  - [.NET](https://learn.microsoft.com/en-us/dotnet/?WT.mc_id=dotnet-35129-website)
### Framework
  - [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-9.0&WT.mc_id=dotnet-35129-website)


## Prerequisites
Before running the project, ensure the following are installed:
### Required
- **.NET 9 SDK** \
Verify with
```bash
dotnet --version
```
- **PostgreSQL 14+** \
Verify with
```bash
psql --version
createdb --version
```


## Quickstart (Development)
1. Create a development appsettings file
Copy the base configurations:
```bash
cp appsettings.json appsettings.Development.json
```

2. Start a PostgreSQL server
If it has been installed as a service:
```bash
sudo service postgresql start
```
Note which port PostgreSQL is running on (usually 5432).

3. Create the database
```bash
sudo -u {username} createdb bmsDB
```
Replace `{username}` with your PostgreSQL username (often postgres).

4. Add a PostgreSQL connection string
Edit `appsettings.Development.json`
```json
  "ConnectionStrings": {
    "PgSQLConnection": "Server=localhost;Port=5432;Database=bmsDB;User Id={username};Password={password};"
  }
```
Replace `{username}` and `{password}` with your PostgreSQL credentials

5. Install EF Core Tools (if not already installed)
```bash
dotnet tool install --global dotnet-ef
``` 

6. Apply Entity framework migrations
```bash
dotnet ef database update
```
This will create the tables in the created database (bmsDB) 
according to the schema defined in `Domain/Entities`

7. Run the development server
```bash
dotnet run --launch-profile https
```
Swagger UI available at:
https://localhost:7090/swagger


## Database Commands
Start/stop PostgreSQL service:
```bash
sudo service postgresql start/restart/stop
sudo service postgresql restart
sudo service postgresql stop
```

Create/drop a database:
```bash
sudo -u {username} createdb bmsDB
sudo -u {username} dropdb bmsDB
```

Interactive PostgreSQL terminal:
```bash
psql -U {username}
```

EF Core Migrations Guide:
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli


## Running Tests

### Unit Tests
To run all unit tests:
```bash
dotnet test tests/UnitTests
```

### Integration Tests
**Prerequisites**
- Docker must be installed
- Docker engine must be running (integration tests use TestContainers)

To run all integration tests:
```bash
dotnet test tests/IntegrationTests
```