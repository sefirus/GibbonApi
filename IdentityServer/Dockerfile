FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5100
EXPOSE 7030
EXPOSE $PORT

# Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IdentityServer/IdentityServer.csproj", "IdentityServer/"]
RUN dotnet restore "IdentityServer/IdentityServer.csproj"
# Copy the whole source except appsettings files
COPY . .
RUN rm -f IdentityServer/appsettings*.json  # Removing appsettings files from the build context
WORKDIR "/src/IdentityServer"
RUN dotnet build "IdentityServer.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "IdentityServer.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]
