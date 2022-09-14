# Build and Run
dotnet restore
dotnet build
dotnet run

# Dev Publish
dotnet publish -c Debug -r linux-x64 --sc false

# Stag Publish
dotnet publish -c Homologacao -r linux-x64 --sc false

# Prod Publish
dotnet publish -c Producao -r linux-x64 --sc false
