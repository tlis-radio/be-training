# Use the official ASP.NET Core 8.0 runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7013
EXPOSE 5247

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/WebApi/WebApi.csproj", "src/WebApi/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Core/Core.csproj", "src/Core/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]

# Restore dependencies
RUN dotnet restore "src/WebApi/WebApi.csproj"

# Copy all files and build the application
COPY . .
WORKDIR "/src/src/WebApi"
RUN dotnet build "WebApi.csproj" -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "WebApi.csproj" -o /app/publish /p:UseAppHost=false

# Final stage: Use the runtime image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
CMD ["dotnet", "WebApi.dll"]