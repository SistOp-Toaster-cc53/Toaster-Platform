FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cafeteria.Toaster.API/Cafeteria.Toaster.API.csproj", "Cafeteria.Toaster.API/"]
RUN dotnet restore "Cafeteria.Toaster.API/Cafeteria.Toaster.API.csproj"
COPY . .
WORKDIR "/src/Cafeteria.Toaster.API"
RUN dotnet build "Cafeteria.Toaster.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Cafeteria.Toaster.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cafeteria.Toaster.API.dll"]
