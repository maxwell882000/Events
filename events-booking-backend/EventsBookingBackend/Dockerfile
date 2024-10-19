# Use ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EventsBookingBackend.csproj", "./"]
RUN dotnet restore "EventsBookingBackend.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "EventsBookingBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EventsBookingBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage - run the application
FROM base AS final
WORKDIR /app

# Ensure the wwwroot folder exists

# Copy the published files from the previous stage
COPY --from=publish /app/publish .

# ENTRYPOINT to run the application
ENTRYPOINT ["dotnet", "EventsBookingBackend.dll"]
