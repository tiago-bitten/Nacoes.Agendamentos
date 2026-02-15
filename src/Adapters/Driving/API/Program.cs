using System.Reflection;
using Hangfire;
using API;
using API.Extensions;
using API.Middlewares;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Suporte ao DATABASE_URL do Railway (converte URI para formato Npgsql)
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]}";
    builder.Configuration["ConnectionStrings:Postgres"] = connectionString;
}

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

await Postgres.DependencyInjection.MigrateDatabaseAsync(app.Services);

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.UseHangfireDashboard();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();

// app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

namespace API
{
    public partial class Program;
}
