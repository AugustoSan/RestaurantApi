FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos del proyecto
COPY ["src/Restaurant.Api/Restaurant.Api.csproj", "src/Restaurant.Api/"]
COPY ["src/Restaurant.Api.Application/Restaurant.Api.Application.csproj", "src/Restaurant.Api.Application/"]
COPY ["src/Restaurant.Api.Core/Restaurant.Api.Core.csproj", "src/Restaurant.Api.Core/"]
COPY ["src/Restaurant.Api.Infrastructure/Restaurant.Api.Infrastructure.csproj", "src/Restaurant.Api.Infrastructure/"]

# Restaura los paquetes
RUN dotnet restore "src/Restaurant.Api/Restaurant.Api.csproj"

# Copia el resto de los archivos
COPY . .

# Publica la aplicación
RUN dotnet publish "src/Restaurant.Api/Restaurant.Api.csproj" -c Release -o /app/publish

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 80

# Copia desde la imagen de compilación
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Restaurant.Api.dll"]