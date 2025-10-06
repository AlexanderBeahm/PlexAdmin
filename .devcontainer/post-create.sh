#!/bin/bash
set -e

echo "🚀 Running post-create setup..."

# Change to the workspace directory
cd /workspaces/PlexAdmin

echo "📦 Restoring .NET packages..."
dotnet restore PlexAdmin/PlexAdmin.csproj

echo "🔧 Verifying EF Core tools..."
dotnet ef --version || echo "⚠️ EF Core tools not found - will try to install"

# Try to install EF tools if not found
if ! dotnet ef --version > /dev/null 2>&1; then
    echo "📥 Installing EF Core tools..."
    dotnet tool install --global dotnet-ef
    export PATH="$PATH:/home/vscode/.dotnet/tools"
fi

echo "✅ Post-create setup completed!"