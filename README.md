# Overview

This application was created for educational purpose and implement basic CRUD operations over two entities. It has the following characteristics and use the resources as described below.

## Specifications and requirements

- Windows
- Visual Studio 17 solution based (.sln)
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

Connection string setup is defined on appsettings.json file, located inside both projects.

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

Just simply build and run on Visual Studio.

# Usage guide manual

This application operates over these two entities: Truck Models and Trucks. The relation between each is 1.N respectively.

## Truck operations

- List all trucks 
- Create a new truck
- Edit a truck
- Delete a truck

## Truck model operations

- List all truck models 
- Create a new truck model
- Edit a truck model
- Delete a truck model (deletes trucks on cascade)

## Truck form fieds

- Manufacturing year (accept only the current year)
- Model year (accept only the current or the next year)
- Truck model (accept only FH ou FM prefixed models)

## Truck model form fied

- Truck Model name
