# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

PlexAdmin is an ASP.NET Core 9.0 Blazor Server application that provides administrative functionality for Plex Media Server that is not available in the standard Plex interface. The application uses ASP.NET Core Identity for user authentication and authorization, Entity Framework Core with SQL Server for data persistence, and integrates with the Plex API to manage playlists and other media server features.

## Technology Stack

- **Framework**: .NET 9.0
- **Web Framework**: ASP.NET Core Blazor Server (Interactive Server Components)
- **Database**: SQL Server (LocalDB for development)
- **ORM**: Entity Framework Core 9.0.9
- **Authentication**: ASP.NET Core Identity with email confirmation required
- **UI**: Bootstrap 5
- **External APIs**: Plex Media Server API
- **JSON Serialization**: Newtonsoft.Json 13.0.4

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
- **Program.cs**: Configures all services, middleware, authentication, and the HTTP request pipeline. This is where dependency injection, Entity Framework, Identity services, Blazor components, and Plex API integration are registered.

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

### Plex Integration Layer
- **Infrastructure/PlexHTTPClient.cs**: Custom HTTP client for Plex API communication
  - Configures base URL, authentication token, and request headers
  - Registered as singleton in DI container
- **Infrastructure/IPlexAPI.cs**: Interface defining Plex API operations
- **Infrastructure/PlexAPI.cs**: Implementation of Plex API client
  - Handles direct HTTP communication with Plex Media Server
  - Deserializes JSON responses into domain models
- **Services/IPlexService.cs**: Interface for business logic layer
- **Services/PlexService.cs**: Service layer that wraps Plex API calls
  - Transforms Plex API models into DTOs for presentation layer
  - Handles error scenarios and provides simplified interface for UI components

### Models
- **Models/Playlist.cs**: Domain model representing a Plex playlist with full properties
- **Models/PlaylistDto.cs**: Data Transfer Object for UI consumption (simplified view)
- **Models/PlexApiResponse.cs**: JSON response models for Plex API deserialization
  - `PlexPlaylistsResponse`: Root response object
  - `MediaContainer`: Container for playlist metadata
  - `PlaylistMetadata`: Individual playlist data with mapping to domain model

### Component Structure
- **App.razor**: Root HTML document, includes Bootstrap, app styles, and Blazor script
- **Routes.razor**: Router configuration with `AuthorizeRouteView` - redirects unauthorized users to login
- **Components/Layout/**: Layout components
  - `MainLayout.razor`: Main application layout
  - `NavMenu.razor`: Navigation menu with links to Home, Counter, Weather, Playlists, and Auth pages
- **Components/Pages/**: Page components
  - `Home.razor`: Landing page
  - `Counter.razor`: Demo counter page
  - `Weather.razor`: Demo weather forecast page
  - `Playlists.razor`: Plex playlists viewer (Interactive Server mode)
  - `Auth.razor`: Authentication-required demo page
  - `Error.razor`: Error handling page
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

### Plex Configuration
- **Plex Token**: Required for API authentication
  - Set via `PLEX_TOKEN` environment variable (preferred for security)
  - Or configure in `appsettings.json` under `Plex:Token`
  - Application will throw exception on startup if not configured
- **Plex Server URL**: Defaults to `http://localhost:32400`
  - Set via `PLEX_URL` environment variable
  - Or configure in `appsettings.json` under `Plex:Url`
- Configuration is loaded in `Program.cs:42-48` and used to initialize `PlexHTTPClient`

### Adding New Features
- **New Pages**: Add `.razor` files to `Components/Pages/` and update `NavMenu.razor` for navigation
- **New Layouts**: Add to `Components/Layout/`
- **New Services**: Register in `Program.cs` with appropriate lifetime (Scoped/Singleton/Transient)
- **Database Models**: Add `DbSet<T>` properties to `ApplicationDbContext` and create migrations
- **User Properties**: Extend `ApplicationUser` class and create a migration
- **Plex API Endpoints**:
  - Add methods to `IPlexAPI` interface and implement in `PlexAPI` class
  - Create corresponding models in `Models/` directory
  - Add service methods in `PlexService` for business logic
  - Inject `IPlexService` into Blazor components and use with `@rendermode InteractiveServer`

### Email Configuration
The application currently uses `IdentityNoOpEmailSender` which doesn't send emails. To enable email functionality:
1. Implement `IEmailSender<ApplicationUser>` with an actual email service
2. Replace the registration in `Program.cs:39`

## Current Plex Features

### Playlists Management
- **View Playlists**: The `/playlists` page displays all playlists from the configured Plex server
  - Shows playlist ID and name in a readonly textarea
  - Supports both regular and smart playlists
  - Handles loading states and error scenarios gracefully
- **API Integration**: Uses layered architecture (PlexAPI → PlexService → Blazor Component)
  - `PlexAPI.GetPlaylists()` retrieves raw playlist data from Plex API endpoint `/playlists`
  - `PlexService.GetPlaylistsAsync()` transforms data into simplified DTOs
  - Component uses Interactive Server render mode for dynamic content

## Future Enhancements

This application is designed to be extended with additional Plex management features not available in the standard Plex interface. Potential areas for expansion include:
- Advanced playlist management (bulk operations, playlist merging)
- Library maintenance tools
- User management and sharing controls
- Custom metadata editing
- Automated media organization
- Analytics and reporting
