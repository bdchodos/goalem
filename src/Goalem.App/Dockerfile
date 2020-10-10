# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# Fetch and install Node 10. Make sure to include the --yes parameter 
# to automatically accept prompts during install, or it'll fail.
RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.csproj ./Goalem.App/
WORKDIR /source/Goalem.App
RUN dotnet restore

# copy everything else and build app
WORKDIR /source
COPY . ./Goalem.App/
WORKDIR /source/Goalem.App
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT dotnet Goalem.App.dll