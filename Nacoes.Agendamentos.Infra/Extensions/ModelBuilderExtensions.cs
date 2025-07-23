using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Converters;
using System.Linq.Expressions;
using System.Reflection;

namespace Nacoes.Agendamentos.Infra.Extensions;

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
                if (genericType == typeof(Id<>))
                {
                    var entityBuilder = modelBuilder.Entity(entityType.ClrType);
                    var argumentType = property.PropertyType.GetGenericArguments()[0];

                    var converterType = typeof(IdValueConverter<>).MakeGenericType(argumentType);
                    var converter = (ValueConverter)Activator.CreateInstance(converterType)!;

                    entityBuilder.Property(property.Name)
                                 .HasConversion(converter);
                }
            }
        }
    }
}
