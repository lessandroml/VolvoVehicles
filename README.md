# Overview

This application was created for educational purpose and implement basic CRUD operations over two entities. It has the following caracteristics and use the resources described below.

## Specifications and requirements

- Visual Studio 17 on Windows 10
- Solution based (.sln)
- C# Project based (.csproj)
- JSON configuration based (.json)
- ASP.Net Core (1.1)
- Ms SQL LocalDb
- Entity Framework Core (1.1)
- Migrations
- ORM
- NUnit (3.13)

**ASP.Net Core Download** (https://dotnet.microsoft.com/en-us/download/dotnet/1.1)

# Technical overview and configurations

Connection string setup is located on appsettings.json, on both projects.

```js
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VolvoVehicles;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  }
}
```

## Storage

The storage file is created at first run. It's generated and located at $HOME/VolvoVehicles.mdf by default.

## Build and Run

Just click and go 4fun.

# Usage guide manual

This application operates over these two entities: Trucks and Truck Models



