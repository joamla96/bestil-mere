# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY RestaurantAPI/RestaurantAPI.csproj RestaurantAPI/
COPY Models/Models.csproj Models/
RUN dotnet restore RestaurantAPI/RestaurantAPI.csproj

# copy everything else and build app
COPY ./RestaurantAPI ./RestaurantAPI/
COPY ./Models ./Models/
WORKDIR /source
RUN dotnet publish RestaurantAPI/RestaurantAPI.csproj -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "RestaurantAPI.dll"]

