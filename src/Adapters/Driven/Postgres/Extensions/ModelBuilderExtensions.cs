using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Postgres.Extensions;

internal static class ModelBuilderExtensions
{
    public static void ApplyValueObjectConverters(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.ClrType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.PropertyType.IsGenericType) continue;

                var genericType = property.PropertyType.GetGenericTypeDefinition();
            }
        }
    }
}
