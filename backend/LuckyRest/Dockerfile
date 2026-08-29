FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/LuckyRest/LuckyRest.csproj", "backend/LuckyRest/"]
COPY ["backend/MapManager/MapManager.csproj", "backend/MapManager/"]
COPY ["backend/WorkshopScraper/WorkshopScraper.csproj", "backend/WorkshopScraper/"]
COPY ["GameHostConveyer/GameHostConveyer.csproj", "GameHostConveyer/"]
RUN dotnet restore "backend/LuckyRest/LuckyRest.csproj"
COPY . .
WORKDIR "/src/backend/LuckyRest"
RUN dotnet build "LuckyRest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LuckyRest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LuckyRest.dll"]
