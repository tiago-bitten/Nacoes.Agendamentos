using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Infra.Extensions;

internal static class ReflectionExtensions
{
    public static bool IsEntityId(this Type type)
        => type.BaseType?.IsGenericType == true && type.BaseType.GetGenericTypeDefinition() == typeof(EntityId<>);
}
