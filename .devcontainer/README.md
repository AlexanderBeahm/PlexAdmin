# PlexAdmin DevContainer Setup

This directory contains the DevContainer configuration for the PlexAdmin ASP.NET Core Blazor application.

## What's Included

- **.NET 9.0** with ASP.NET Core runtime
- **SQL Server 2022 Developer Edition** in a separate container
- **Entity Framework Core tools** pre-installed globally
- **VS Code extensions** for C#, Blazor, and SQL Server development

## Getting Started

1. Open the project in VS Code
2. When prompted, click "Reopen in Container" or use `Ctrl+Shift+P` → "Dev Containers: Reopen in Container"
3. VS Code will build the containers and set up the development environment
4. The database will be automatically created and migrations applied via `postStartCommand`

## Services

- **App Container**: Your development environment with .NET 9 and all tools
- **SQL Server Container**: Runs on port 1433 with SA password: `YourStrong!Passw0rd`

## Database Connection

The development configuration uses SQL Server running in the container:
- **Server**: localhost,1433
- **Database**: PlexAdmin
- **Username**: sa
- **Password**: YourStrong!Passw0rd

## Docker Access

To use your host Docker installation from within the devcontainer:

1. **From VS Code Terminal**: Open a new terminal in VS Code (outside the container)
2. **From WSL2 Terminal**: Use your normal WSL2 terminal with Docker commands
3. **Container Terminal**: The devcontainer focuses on .NET development - use host terminals for Docker operations

## Common Commands

```bash
# Build the project
dotnet build PlexAdmin/PlexAdmin.csproj

# Run the application
dotnet run --project PlexAdmin/PlexAdmin.csproj

# Create and apply migrations
dotnet ef migrations add MigrationName --project PlexAdmin/PlexAdmin.csproj
dotnet ef database update --project PlexAdmin/PlexAdmin.csproj
```

## Ports

- **5000/5001**: ASP.NET Core application (HTTP/HTTPS)
- **1433**: SQL Server database

## Security Notes

- The SA password is set for development purposes only
- In production, use proper authentication and secure passwords
- The `TrustServerCertificate=True` setting is for development only

## Troubleshooting

If you encounter network issues during container build:
- Try rebuilding with `Dev Containers: Rebuild Container`
- The configuration avoids problematic network-dependent features
- Focus is on .NET development with containerized SQL Server