FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

COPY . .

# Publica o projeto da API
RUN dotnet restore "src/Adapters/Driving/API/API.csproj"
RUN dotnet publish "src/Adapters/Driving/API/API.csproj" -c Release -o /App

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /App

# Instala pacotes adicionais
RUN apt-get update && apt-get install -y libgdiplus
RUN sed -i 's/^Components: main$/& contrib/' /etc/apt/sources.list.d/debian.sources
RUN apt-get update; apt-get install -y ttf-mscorefonts-installer fontconfig

# Copia a aplicação do build
COPY --from=build-env /App .

# Define o ponto de entrada da API
ENTRYPOINT ["dotnet", "API.dll"]
