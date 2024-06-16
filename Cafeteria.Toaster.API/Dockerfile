# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia el archivo del proyecto y restaura las dependencias
COPY ["Cafeteria.Toaster.API.csproj", "./"]
RUN dotnet restore "Cafeteria.Toaster.API.csproj" --disable-parallel

# Copia el resto de los archivos del proyecto y realiza la publicación
COPY . .
RUN dotnet publish "Cafeteria.Toaster.API.csproj" -c Release -o /app/publish

# Etapa de tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia los archivos publicados desde la etapa de construcción
COPY --from=build /app/publish .

# Expone el puerto en el que se ejecuta la aplicación
EXPOSE 5000

# Define el comando de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Cafeteria.Toaster.API.dll"]
