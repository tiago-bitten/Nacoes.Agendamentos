using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.IoC;
using Nacoes.Agendamentos.Infra.Contexts;
using Nacoes.Agendamentos.Infra.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(type => type.FullName?.Replace("+", ".")));

builder.Services.AddAppConfiguration(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddAppHandlers();
builder.Services.AddAppQueries();
builder.Services.AddValidators();
builder.Services.AddFactories();
builder.Services.AddServices();

builder.Services.Configure<RouteOptions>(x => x.LowercaseUrls = true);

var authSettings = builder.Configuration.GetSection("Authentication").Get<AuthenticationSettings>();
builder.Services.AddJwt(authSettings!.Jwt.Issuer, authSettings.Jwt.Audience, authSettings.Jwt.Secret);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var devSettings = app.Configuration.GetSection("Dev").Get<DevSettings>() ?? new DevSettings();

    if (devSettings.RecriarBanco)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<NacoesDbContext>();

        var entityTypes = context.Model.GetEntityTypes();

        foreach (var type in entityTypes)
        {
            var tableName = type.GetTableName();
            var sql = $"TRUNCATE TABLE {tableName} CASCADE;";
            context.Database.ExecuteSqlRaw(sql);
        }

        context.SaveChanges();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
