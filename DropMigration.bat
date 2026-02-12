@echo off
echo Removendo a ultima migracao...
dotnet ef migrations remove --verbose --context NacoesDbContext --project ".\src\Adapters\Driven\Postgres\Postgres.csproj" --startup-project ".\src\Adapters\Driving\API\API.csproj"
pause
