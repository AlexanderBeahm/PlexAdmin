#!/bin/bash

echo "Creation script running..."

# Add GitHub to known_hosts so first Git call is non-interactive:
mkdir -p ~/.ssh
ssh-keyscan -t rsa,ecdsa,ed25519 github.com >> ~/.ssh/known_hosts

# Change to the workspace directory
cd /workspaces/PlexAdmin

echo "Restoring .NET packages..."
dotnet restore PlexAdmin/PlexAdmin.csproj

echo "Verifying EF Core tools..."
dotnet ef --version || echo "EF Core tools not found - will try to install"

# Try to install EF tools if not found
if ! dotnet ef --version > /dev/null 2>&1; then
    echo "📥 Installing EF Core tools..."
    dotnet tool install --global dotnet-ef
    export PATH="$PATH:/home/vscode/.dotnet/tools"
fi

echo "✓ Creation script completed!"