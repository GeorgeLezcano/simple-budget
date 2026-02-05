#!/usr/bin/env bash
set -e

dotnet publish app/app.csproj \
  -c Release \
  -r win-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:EnableCompressionInSingleFile=true \
  -p:DebugType=none \
  -p:DebugSymbols=false \
  -o release

shopt -s nullglob
for f in release/*; do
  [[ "$f" == *.exe ]] || rm -f "$f"
done

echo "✅ Executable created in ./release/"