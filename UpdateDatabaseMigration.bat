@echo off
cd..
echo Executando migrations: dotnet ef database update --verbose --context NacoesDbContext --project ".\Nacoes.Agendamentos\Nacoes.Agendamentos.Infra\Nacoes.Agendamentos.Infra.csproj" --startup-project ".\Nacoes.Agendamentos.API\Nacoes.Agendamentos\Nacoes.Agendamentos.API.csproj"
dotnet ef database update --verbose --context NacoesDbContext --project ".\Nacoes.Agendamentos\Nacoes.Agendamentos.Infra\Nacoes.Agendamentos.Infra.csproj" --startup-project ".\Nacoes.Agendamentos\Nacoes.Agendamentos.API\Nacoes.Agendamentos.API.csproj"
pause
