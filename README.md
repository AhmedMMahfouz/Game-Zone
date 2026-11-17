# Game Zone

Game Zone is an ASP.NET Core MVC application for browsing and managing a video game catalog. It demonstrates a layered MVC structure, Entity Framework Core, SQL Server, server-side validation, image uploads, and a many-to-many relationship between games and supported devices.

## Features

- Browse the game catalog and view game details.
- Create, edit, and delete games.
- Assign a category to each game.
- Associate each game with one or more gaming devices.
- Upload and preview game cover images.
- Validate game names, descriptions, cover file extensions, and file sizes.
- Persist data with Entity Framework Core Code First migrations.

## Tech Stack

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core 8
- SQL Server
- C#
- Bootstrap, Bootstrap Icons, jQuery Validation

## Architecture

The application uses a simple separation of concerns:

- **Controllers** handle HTTP requests and coordinate application operations.
- **Services** contain game, category, and device operations.
- **ViewModels** define the data accepted by the MVC forms.
- **Entity configurations** define database relationships, constraints, and seed data.
- **Custom validation attributes** validate uploaded cover files.

The data model includes a many-to-many relationship between `Game` and `Device`, implemented through `GameDevice`. Each game also belongs to a category.

## Screenshots

### Games catalog
![Games catalog](Project%20ScreenShots/Home%20Index%20View.png)

### Create a game
![Create game](Project%20ScreenShots/Create%20New%20Game.png)

### Game operations
![Game operations](Project%20ScreenShots/Operation%20On%20Game%20.png)

### Update a game
![Update game](Project%20ScreenShots/Update%20The%20Game%20.png)

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server LocalDB (or another SQL Server instance)
- Visual Studio 2022 or another .NET-compatible IDE

### Configure the database

The default connection string is configured in `appsettings.json` for SQL Server LocalDB:

```json
{
  "ConnectionStrings": {
    "defaultConnection": "server=(localdb)\\ProjectModels;Database=GameHub;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

Update the connection string to match your local SQL Server instance when needed. For shared or production environments, keep connection strings in User Secrets or environment variables rather than committing credentials.

### Run migrations

From the directory containing `GameZone.csproj`, run:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

If the EF Core CLI is not installed:

```bash
dotnet tool install --global dotnet-ef --version 8.0.11
```

## Notes

- Uploaded cover images are stored under `wwwroot/assets/Image/Games`.
- The project includes EF Core migrations in the `Migrations` directory.
- The current sample connection string uses LocalDB and may need adjustment on non-Windows environments.
