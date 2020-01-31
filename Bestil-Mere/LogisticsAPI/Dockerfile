# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY LogisticsAPI/LogisticsAPI.csproj LogisticsAPI/
COPY Models/Models.csproj Models/
RUN dotnet restore LogisticsAPI/LogisticsAPI.csproj

# copy everything else and build app
COPY . ./
WORKDIR /source
RUN dotnet publish LogisticsAPI/LogisticsAPI.csproj -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "LogisticsAPI.dll"]