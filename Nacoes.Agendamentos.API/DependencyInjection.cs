using Microsoft.OpenApi.Models;
using Nacoes.Agendamentos.API.Json;
using Nacoes.Agendamentos.API.Middlewares;

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
                    x.JsonSerializerOptions.Converters.Add(new IdJsonConverterFactory());
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

        // TODO: Remover GlobalExceptionHandlerMiddleware e substituir por IExceptionHandler
        // services.AddExceptionHandler<GlobalExceptionHandler>();
        
        return services;
    }
}