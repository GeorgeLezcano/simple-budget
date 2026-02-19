#!/usr/bin/env bash
set -Eeuo pipefail

NAME="${1:-}"
if [[ -z "$NAME" ]]; then
  echo "Usage: $0 <MigrationName>"
  exit 1
fi

APP_DIR="app"

if [[ ! -f "$APP_DIR/app.csproj" ]]; then
  echo "❌ Could not find project file at $APP_DIR/app.csproj"
  exit 1
fi

pushd "$APP_DIR" >/dev/null

# Ensure dotnet-ef is available
if ! command -v dotnet-ef >/dev/null 2>&1; then
  echo "dotnet-ef not found. Installing globally..."
  dotnet tool install -g dotnet-ef
  export PATH="$PATH:$HOME/.dotnet/tools"
fi

echo "Adding EF Core migration: $NAME"
dotnet ef migrations add "$NAME" \
  --project "app.csproj" \
  --output-dir "Data/Migrations"

popd >/dev/null

echo ""
echo "Building app..."
dotnet build "$APP_DIR/app.csproj"

echo ""
echo "✅ Done."
echo "Commit the new files under:"
echo "  app/Data/Migrations/"
echo ""
echo "Note: No database update was run. App will apply migrations on startup."
