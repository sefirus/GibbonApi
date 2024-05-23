# Use the official Microsoft .NET image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE $PORT

# Build the application.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Host/Host.csproj", "Host/"]
RUN dotnet restore "Host/Host.csproj"
# Copy the whole source except appsettings files
COPY . .
RUN rm -f Host/appsettings*.json  # Removing appsettings files from the build context
WORKDIR "/src/Host"
RUN dotnet build "Host.csproj" -c Release -o /app/build

# Publish the application.
FROM build AS publish
RUN dotnet publish "Host.csproj" -c Release -o /app/publish

# Final stage/image.
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Host.dll"]
