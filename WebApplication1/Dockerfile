#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /app
#
#COPY . .
#RUN dotnet restore
#RUN dotnet publish -c Release -o out
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /app
#
#COPY --from=build /app/out .
#
#ENV ASPNETCORE_URLS=http://+:80
#ENV ASPNETCORE_ENVIRONMENT=Development
#
#EXPOSE 80
#
#ENTRYPOINT ["dotnet", "WebApplication1.dll"]

# Étape 1 : build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie et restore les dépendances
COPY *.csproj .
RUN dotnet restore

# Copie tout le code et compile
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Étape 2 : image finale (plus légère, sans le SDK)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
