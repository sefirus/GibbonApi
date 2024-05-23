# Use the official Microsoft .NET image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build the application.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Host/Host.csproj", "Host/"]
RUN dotnet restore "Host/Host.csproj"
COPY . .
WORKDIR "/src/Host"
RUN dotnet build "Host.csproj" -c Release -o /app/build

# Publish the application to a specific subdirectory.
FROM build AS publish
RUN dotnet publish "Host.csproj" -c Release -o /app/publish/host

# Final stage/image.
FROM base AS final
WORKDIR /app
# Ensure to copy from the specific subdirectory.
COPY --from=publish /app/publish/host .
ENTRYPOINT ["dotnet", "Host.dll"]
