# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Gateway/Gateway.csproj Gateway/
RUN dotnet restore Gateway/Gateway.csproj

# copy everything else and build app
COPY . ./
WORKDIR /source
RUN dotnet publish Gateway/Gateway.csproj -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Gateway.dll"]

