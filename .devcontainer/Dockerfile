# Use the newer devcontainer .NET image - this should have vscode user already
FROM mcr.microsoft.com/devcontainers/dotnet:dev-9.0

# Switch to vscode user (should already exist)
USER vscode

# Add .NET tools to PATH for vscode user
ENV PATH="$PATH:/home/vscode/.dotnet/tools"

# Set the working directory
WORKDIR /workspaces