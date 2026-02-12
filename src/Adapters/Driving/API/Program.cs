using System.Reflection;
using Hangfire;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using API;
using API.Extensions;
using API.Middlewares;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddProjectDependencies(builder.Configuration)
    .AddApi();

builder.Services.AddCors(x => x.AddDefaultPolicy(option =>
    option.AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials()
));


builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// app.UseHealthChecks("/health", new HealthCheckOptions
// {
//     Predicate = _ => true,
// });

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.UseHangfireDashboard();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

// app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

namespace API
{
    public partial class Program;
}
