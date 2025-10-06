# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

PlexAdmin is an ASP.NET Core 9.0 Blazor Server application with ASP.NET Core Identity for user authentication and authorization. The application uses Entity Framework Core with SQL Server for data persistence.

## Technology Stack

- **Framework**: .NET 9.0
- **Web Framework**: ASP.NET Core Blazor Server (Interactive Server Components)
- **Database**: SQL Server (LocalDB for development)
- **ORM**: Entity Framework Core 9.0.9
- **Authentication**: ASP.NET Core Identity with email confirmation required
- **UI**: Bootstrap 5

## Key Commands

### Build and Run
```bash
# Build the project
dotnet build PlexAdmin/PlexAdmin.csproj

# Run the application (development)
dotnet run --project PlexAdmin/PlexAdmin.csproj

# Run with specific profile
dotnet run --project PlexAdmin/PlexAdmin.csproj --launch-profile https
```

### Database Management
```bash
# Add a new migration
dotnet ef migrations add <MigrationName> --project PlexAdmin/PlexAdmin.csproj

# Update database to latest migration
dotnet ef database update --project PlexAdmin/PlexAdmin.csproj

# Remove last migration (if not applied)
dotnet ef migrations remove --project PlexAdmin/PlexAdmin.csproj

# Drop database
dotnet ef database drop --project PlexAdmin/PlexAdmin.csproj
```

### Testing
```bash
# Run all tests (if test project exists)
dotnet test

# Run tests in specific test project
dotnet test <TestProjectPath>
```

## Architecture

### Application Entry Point
- **Program.cs**: Configures all services, middleware, authentication, and the HTTP request pipeline. This is where dependency injection, Entity Framework, Identity services, and Blazor components are registered.

### Authentication & Identity
- Uses ASP.NET Core Identity with custom `ApplicationUser` (extends `IdentityUser`)
- Email confirmation is **required** for sign-in (`RequireConfirmedAccount = true`)
- Identity scaffolded pages are in `Components/Account/Pages/`
- Custom Identity services:
  - `IdentityUserAccessor`: Provides access to the current user
  - `IdentityRedirectManager`: Manages redirects after authentication actions
  - `IdentityRevalidatingAuthenticationStateProvider`: Revalidates authentication state periodically
  - `IdentityNoOpEmailSender`: No-op email sender (replace with real implementation for production)
  - `IdentityComponentsEndpointRouteBuilderExtensions`: Maps additional Identity endpoints

### Data Layer
- **ApplicationDbContext**: EF Core DbContext inheriting from `IdentityDbContext<ApplicationUser>`
- **ApplicationUser**: Custom user entity (currently extends `IdentityUser` without additional properties)
- **Migrations**: Located in `Data/Migrations/`
- **Connection String**: Configured in `appsettings.json`, uses LocalDB by default

### Component Structure
- **App.razor**: Root HTML document, includes Bootstrap, app styles, and Blazor script
- **Routes.razor**: Router configuration with `AuthorizeRouteView` - redirects unauthorized users to login
- **Components/Layout/**: Layout components (`MainLayout.razor`, `NavMenu.razor`)
- **Components/Pages/**: Page components (Home, Counter, Weather, Auth, Error)
- **Components/Account/**: All Identity-related Blazor components
  - **Pages/**: Login, Register, Manage account, 2FA, Password reset, etc.
  - **Shared/**: Reusable components like `ManageLayout`, `StatusMessage`, `ExternalLoginPicker`

### Blazor Configuration
- **Render Mode**: Interactive Server (SignalR-based, stateful connections)
- **Authorization**: Enforced at router level - all routes require authentication by default unless marked with `[AllowAnonymous]`
- **Static Assets**: Mapped with `.MapStaticAssets()`
- **Antiforgery**: Enabled via `.UseAntiforgery()`

## Development Notes

### Running the Application
- Default URLs: `https://localhost:7132` (HTTPS) or `http://localhost:5136` (HTTP)
- In Development mode, uses migrations endpoint (`app.UseMigrationsEndPoint()`) for applying migrations from the browser
- Production uses exception handler middleware instead

### Database
- LocalDB connection string is in `appsettings.json`
- Database name: `aspnet-PlexAdmin-a9975096-c897-4ced-bb6e-300964aa0f31`
- User secrets ID: `aspnet-PlexAdmin-a9975096-c897-4ced-bb6e-300964aa0f31`

### Adding New Features
- **New Pages**: Add `.razor` files to `Components/Pages/`
- **New Layouts**: Add to `Components/Layout/`
- **New Services**: Register in `Program.cs` with appropriate lifetime (Scoped/Singleton/Transient)
- **Database Models**: Add `DbSet<T>` properties to `ApplicationDbContext` and create migrations
- **User Properties**: Extend `ApplicationUser` class and create a migration

### Email Configuration
The application currently uses `IdentityNoOpEmailSender` which doesn't send emails. To enable email functionality:
1. Implement `IEmailSender<ApplicationUser>` with an actual email service
2. Replace the registration in `Program.cs:36`
