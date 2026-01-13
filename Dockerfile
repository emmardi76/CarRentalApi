# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia sólo el csproj primero para aprovechar la cache de restore
COPY ["CarRentalApi.csproj", "./"]
RUN dotnet restore "CarRentalApi.csproj"

# Copia el resto del código y publica
COPY . .
RUN dotnet publish "CarRentalApi.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_RUNNING_IN_CONTAINER=true

COPY --from=build /app/publish .
EXPOSE 80

ENTRYPOINT ["dotnet", "CarRentalApi.dll"]