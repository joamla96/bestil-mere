# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY OrderAPI/OrderAPI.csproj OrderAPI/
COPY Models/Models.csproj Models/
RUN dotnet restore OrderAPI/OrderAPI.csproj

# copy everything else and build app
COPY ./OrderAPI ./OrderAPI/
COPY ./Models ./Models/
WORKDIR /source
RUN dotnet publish OrderAPI/OrderAPI.csproj -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "OrderAPI.dll"]

