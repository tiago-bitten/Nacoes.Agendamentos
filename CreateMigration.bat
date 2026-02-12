@echo off
setlocal

:: Nome da Migration
set /p MigrationName=Digite o nome da migration:

:: Caminhos do projeto
set StartupProjectPath=.\src\Adapters\Driving\API\API.csproj
set RepositoryProjectPath=.\src\Adapters\Driven\Postgres\Postgres.csproj
set DbContextName=NacoesDbContext

:: Comando para criar a migration
echo Criando migration: %MigrationName%
dotnet ef migrations add %MigrationName% --context %DbContextName% --project "%RepositoryProjectPath%" --startup-project "%StartupProjectPath%" --verbose

pause
