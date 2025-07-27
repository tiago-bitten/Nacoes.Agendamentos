@echo off
cd..
echo Removendo a ultima migracao: dotnet ef migrations remove --verbose --context NacoesDbContext --project ".\Nacoes.Agendamentos\Nacoes.Agendamentos.Infra\Nacoes.Agendamentos.Infra.csproj" --startup-project ".\Nacoes.Agendamentos.API\Nacoes.Agendamentos.API.csproj"
dotnet ef migrations remove --verbose --context NacoesDbContext --project ".\Nacoes.Agendamentos\Nacoes.Agendamentos.Infra\Nacoes.Agendamentos.Infra.csproj" --startup-project ".\Nacoes.Agendamentos\Nacoes.Agendamentos.API\Nacoes.Agendamentos.API.csproj"
pause