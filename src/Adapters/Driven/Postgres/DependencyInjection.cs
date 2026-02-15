using Application.Shared.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Postgres.Contexts;
using Postgres.DomainEvents;

namespace Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresAdapter(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<NacoesDbContext>(options =>
            options.UseNpgsql(connectionString)
                   .UseSnakeCaseNamingConvention());

        services.AddScoped<INacoesDbContext>(sp => sp.GetRequiredService<NacoesDbContext>());
        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

        return services;
    }

    public static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NacoesDbContext>();
        var pendingMigrations = (await context.Database.GetPendingMigrationsAsync()).ToList();

        if (pendingMigrations.Count == 0)
        {
            return;
        }

        await context.Database.MigrateAsync();
    }
}
