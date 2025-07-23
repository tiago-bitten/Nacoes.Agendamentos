using Hangfire;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Nacoes.Agendamentos.API;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Json;
using Nacoes.Agendamentos.API.Middlewares;
using Nacoes.Agendamentos.Application;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Common.Settings;
using Nacoes.Agendamentos.Infra;
using Nacoes.Agendamentos.Infra.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
       .AddApplication()
       .AddPresentation()
       .AddInfra(builder.Configuration);


var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nacoes.Agendamentos API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHangfireDashboard();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
