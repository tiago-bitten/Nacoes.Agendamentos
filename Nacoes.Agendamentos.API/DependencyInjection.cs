using Microsoft.OpenApi.Models;

namespace Nacoes.Agendamentos.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();
        services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

        services.Configure<RouteOptions>(x => x.LowercaseUrls = true);

        services.AddCors(x => x.AddDefaultPolicy(option =>
            option.AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(_ => true)
                  .AllowCredentials()
        ));

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Nacoes.Agendamentos API",
                Version = "v1"
            });

            c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

            // 🔑 Definição do esquema de segurança
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey, // <-- IMPORTANTE: tem que ser ApiKey aqui
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Insira o token JWT desta forma: Bearer {seu token}"
            });

            // 🔒 Requisito global de segurança
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}
