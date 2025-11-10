# building-management-system

A cloud-based building management system designed to streamline 
daily operations and tasks for property managers and building 
owners by centralising work order tracking, contractor management
and building management in one platform.


## About the Project
This project aims to develop a cloud based SaaS building management
system designed for property managers, building owners, and small 
real estate agencies. Many industry clients currently rely on 
platforms such as MyBos, BuildingLink and StrataMaster, which are 
often expensive for smaller agents. This solution seeks to provide 
a more affordable, lightweight alternative while maintaining core 
common functionalities such as building management, maintenance 
tracking, and contractor management. In the future, a mobile app 
may be introduced to improve accessibility. The system is targeted 
toward small to medium property management firms, with a scalable 
design that can later be suitable for larger enterprises.


### Features

#### Building Dashboard
Provides an overview of all buildings entered into the system and their key information.

#### Building Management
Allows users to add, edit and manage building details.

#### Work Orders / Maintenance Requests
Enables creation and tracking of work orders and maintenance requests for managed buildings.

#### Contractor Dashboard
Provides an overview of contractors entered into the system and their key information.

#### Contractor Management
Allows users to add, edit and manage contractors.

#### User Authentication
Secure login system which allows authorized users to only access relevant data and operations.

## Getting Started

### Prerequisites
Clone the project
```bash
git clone https://github.com/Aldebaran20/building-management-system.git
```

<!-- ### Environment Variables
To run the project backend with Docker, you will need to add the 
following environment variables to a .env file in the backend:
- `POSTGRES_HOST`
- `POSTGRES_USER`
- `POSTGRES_PASSWORD`
- `POSTGRES_DB` -->

### Running Frontend
```bash
cd frontend
```

To run development server
```bash
npm install
npm run dev
```

### Running Backend
```bash
cd backend
```

To run development server
```bash
dotnet run --launch-profile https
```

### Running Tests
No test suites currently set up on frontend or backend.

<!-- ### Instructions to configure
Can configure tsconfig and linting -->


## Technology Stack/Documentation

### Client
- Language
  - [Typescript](https://www.typescriptlang.org/docs/)
- Framework
  - [React](https://react.dev/learn)
- Libraries
  - [React Router](https://reactrouter.com/home)
  - [Tailwind w/Vite](https://tailwindcss.com/docs/installation/using-vite)
- Tools
  - [Vite](https://vitejs.dev/)

### Server
- Language
  - [C#](https://learn.microsoft.com/en-us/dotnet/csharp/?WT.mc_id=dotnet-35129-website)
  - [.NET](https://learn.microsoft.com/en-us/dotnet/?WT.mc_id=dotnet-35129-website)
- Framework
  - [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-9.0&WT.mc_id=dotnet-35129-website)
<!-- - Libraries
  - [Kysely](https://kysely.dev/docs/intro) -->

### Database
- [PostgreSQL](https://www.postgresql.org/docs/current/)

### DevOps
- [Docker](https://docs.docker.com/manuals/)

<!-- ### Libraries
- [DotEnv](https://www.npmjs.com/package/dotenv) -->


## Known issues
No known technical issues so far as the project is early in the
development stages.


## Commit Conventions

[How to write a Git commit message](https://cbea.ms/git-commit/)
### Summary
1. Separate subject from body with a blank line
2. Limit the subject line to 50 characters
3. Capitalize the subject line
4. Do not end the subject line with a period
5. Use the imperative mood in the subject line
6. Wrap the body at 72 characters
7. Use the body to explain what and why vs. how

Flexible usage of [Conventional commits](https://www.conventionalcommits.org/en/v1.0.0/).
- Should use scope, e.g. [frontend], [backend]
- Optional usage of commit types, e.g. fix, feat, docs, style