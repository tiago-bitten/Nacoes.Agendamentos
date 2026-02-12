@echo off
echo Executando migrations...
dotnet ef database update --verbose --context NacoesDbContext --project ".\src\Adapters\Driven\Postgres\Postgres.csproj" --startup-project ".\src\Adapters\Driving\API\API.csproj"
pause
