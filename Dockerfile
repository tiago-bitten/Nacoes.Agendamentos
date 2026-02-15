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
RUN if [ -f /etc/apt/sources.list.d/debian.sources ]; then \
      sed -i 's/^Components: main$/& contrib/' /etc/apt/sources.list.d/debian.sources; \
    elif [ -f /etc/apt/sources.list.d/ubuntu.sources ]; then \
      sed -i 's/^Components: main/& contrib/' /etc/apt/sources.list.d/ubuntu.sources; \
    elif [ -f /etc/apt/sources.list ]; then \
      sed -i 's/^\(deb .* main\)$/\1 contrib/' /etc/apt/sources.list; \
    fi
RUN apt-get update; apt-get install -y ttf-mscorefonts-installer fontconfig

# Copia a aplicação do build
COPY --from=build-env /App .

# Railway atribui a porta via $PORT; default 8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_HTTP_PORTS=8080

# Ponto de entrada compatível com Railway ($PORT dinâmico)
ENTRYPOINT ["sh", "-c", "dotnet API.dll --urls http://+:${PORT:-8080}"]
