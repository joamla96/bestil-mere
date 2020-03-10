# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# Setup NodeJs
RUN apt-get update -qq && \
    apt-get install -qq -y wget && \
    apt-get install -qq -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -qq -y build-essential nodejs
# End setup

# copy csproj and restore as distinct layers
COPY Frontend/Frontend.csproj Frontend/
COPY Models/Models.csproj Models/
RUN dotnet restore Frontend/Frontend.csproj

# copy everything else and build app
COPY ./Frontend ./Frontend/
COPY ./Models ./Models/
WORKDIR /source
RUN dotnet publish Frontend/Frontend.csproj -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Frontend.dll"]

