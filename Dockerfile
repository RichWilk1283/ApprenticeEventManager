# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

# Copy the project file and restore dependencies (optimized for caching)
COPY ["ApprenticeEventManager.csproj", "./"]
RUN dotnet restore

# Copy everything else and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
EXPOSE 80
COPY --from=build /App/out .

# Set the entry point
ENTRYPOINT ["dotnet", "ApprenticeEventManager.dll"]