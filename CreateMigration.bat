@echo off
setlocal

:: Nome da Migration
set /p MigrationName=Digite o nome da migration: 

:: Caminhos do projeto
set StartupProjectPath=.\Nacoes.Agendamentos.API\Nacoes.Agendamentos.API.csproj
set RepositoryProjectPath=.\Nacoes.Agendamentos.Infra\Nacoes.Agendamentos.Infra.csproj
set DbContextName=NacoesDbContext

:: Comando para criar a migration
echo Criando migration: %MigrationName%
dotnet ef migrations add %MigrationName% --context %DbContextName% --project "%RepositoryProjectPath%" --startup-project "%StartupProjectPath%" --verbose

pause
