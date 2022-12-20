# Overview

This application was created for educational purpose and implements basic CRUD operations on two entities. It has the following characteristics and uses the features described below.

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

Connection string setup is defined in appsettings.json file, located inside both projects.

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

The storage file is created on first run. It's generated and located in $HOME/VolvoVehicles.mdf by default.

## Build and Run

Just simply build and run on Visual Studio.

# Usage guide manual

This application operates on these two entities: Truck Models and Trucks. The relation between each is 1.N respectively.

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

- Manufacturing year (accept the current year only)
- Model year (accept only the current or the next year)
- Truck model (only accept models with FH or FM prefix)

## Truck model form fied

- Truck Model name
