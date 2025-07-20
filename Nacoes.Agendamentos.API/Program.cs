using Hangfire;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Json;
using Nacoes.Agendamentos.API.Middlewares;
using Nacoes.Agendamentos.Application.Common.Settings;
using Nacoes.Agendamentos.Infra.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.PropertyNamingPolicy = null;
                    x.JsonSerializerOptions.Converters.Add(new IdJsonConverterFactory());
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAppConfiguration(builder.Configuration);
builder.Services.AddDatabase();
builder.Services.AddRepositories();
builder.Services.AddAppHandlers();
builder.Services.AddValidators();
builder.Services.AddFactories();
builder.Services.AddServices();
builder.Services.AddHangfire();
builder.Services.AddHangfireServer();

builder.Services.Configure<RouteOptions>(x => x.LowercaseUrls = true);

var authSettings = builder.Configuration.GetSection("Authentication").Get<AuthenticationSettings>();
builder.Services.AddJwt(authSettings!.Jwt.Issuer, authSettings.Jwt.Audience, authSettings.Jwt.Secret);

builder.Services.AddCors(x => x.AddDefaultPolicy(option =>
    option.AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(_ => true)
          .AllowCredentials()
));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nacoes.Agendamentos API", Version = "v1" });
    c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

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
    var devSettings = app.Configuration.GetSection("Dev").Get<DevSettings>();
    if (devSettings!.RecriarBanco)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NacoesDbContext>();
        var entityTypes = context.Model.GetEntityTypes();

        foreach (var type in entityTypes)
        {
            var tableName = type.GetTableName();
            var sql = $"TRUNCATE TABLE {tableName} CASCADE;";
            await context.Database.ExecuteSqlRawAsync(sql);
        }
        await context.SaveChangesAsync();
    }
}

app.UseHangfireDashboard();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
