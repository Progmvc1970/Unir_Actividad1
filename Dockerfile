# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY MinimalApiGestionDeAlquilerDeVehiculos/*.csproj ./
RUN dotnet restore

# Copiar el resto del código
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime-env
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "MinimalApiGestionDeAlquilerDeVehiculos.dll"]
