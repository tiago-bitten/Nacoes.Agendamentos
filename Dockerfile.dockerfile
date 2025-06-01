# Use a imagem oficial do .NET Core 3.1 como base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# Defina o diret�rio de trabalho
WORKDIR /app

# Copie os arquivos do projeto para o diret�rio de trabalho
COPY *.csproj ./

# Restaure as depend�ncias do projeto
RUN dotnet restore

# Copie o restante dos arquivos do projeto para o diret�rio de trabalho
COPY . .

# Compile o projeto
RUN dotnet build -c Release -o out

# Exponha a porta 80 para o container
EXPOSE 80

# Defina o comando para executar o aplicativo
CMD ["dotnet", "out/Nacoes.Agendamentos.API.dll"]