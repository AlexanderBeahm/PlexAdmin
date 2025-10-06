# Use the official .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY PlexAdmin/PlexAdmin.csproj PlexAdmin/
RUN dotnet restore PlexAdmin/PlexAdmin.csproj

# Copy the rest of the application code
COPY PlexAdmin/ PlexAdmin/

# Build the application
WORKDIR /src/PlexAdmin
RUN dotnet build PlexAdmin.csproj -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish PlexAdmin.csproj -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 9.0 runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy the published application from the publish stage
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "PlexAdmin.dll"]
