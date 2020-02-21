# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY CustomerAPI/CustomerAPI.csproj CustomerAPI/
COPY Models/Models.csproj Models/
RUN dotnet restore CustomerAPI/CustomerAPI.csproj

# copy everything else and build app
COPY ./CustomerAPI ./CustomerAPI/
COPY ./Models ./Models/WORKDIR /source
RUN dotnet publish CustomerAPI/CustomerAPI.csproj -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "CustomerAPI.dll"]

