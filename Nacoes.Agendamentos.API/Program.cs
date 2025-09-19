using System.Reflection;
using Hangfire;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Nacoes.Agendamentos.API;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Middlewares;
using Nacoes.Agendamentos.Application;
using Nacoes.Agendamentos.Infra;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddApplication()
    .AddInfra(builder.Configuration)
    .AddApi();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
});

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.UseHangfireDashboard();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

namespace Seeds.API
{
    public partial class Program;
}