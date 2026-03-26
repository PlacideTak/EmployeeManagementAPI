# Étape 1 : build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier les fichiers de solution et restaurer
COPY *.sln .
COPY WebApplication1/*.csproj ./WebApplication1/
COPY EmployeeManagement.Tests/*.csproj ./EmployeeManagement.Tests/
RUN dotnet restore

# Copier tout le code et build
COPY . .
RUN dotnet publish WebApplication1/WebApplication1.csproj -c Release -o /app/publish

# Étape 2 : runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]