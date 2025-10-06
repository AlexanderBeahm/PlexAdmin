#!/bin/bash
set -e

echo "🎯 Running post-start setup..."

# Change to the workspace directory
cd /workspaces/PlexAdmin

echo "⏳ Waiting for SQL Server to be ready..."
sleep 15

echo "🗄️ Checking database connection..."
# Simple connection test
timeout 30 bash -c 'until printf "" 2>>/dev/null >>/dev/tcp/localhost/1433; do sleep 1; done'

echo "🔄 Running database migrations..."
export PATH="$PATH:/home/vscode/.dotnet/tools"

if dotnet ef database update --project PlexAdmin/PlexAdmin.csproj --verbose; then
    echo "✅ Database migrations completed successfully!"
else
    echo "❌ Database migration failed. You can run this manually later:"
    echo "   dotnet ef database update --project PlexAdmin/PlexAdmin.csproj"
fi

echo "🎉 Post-start setup completed!"