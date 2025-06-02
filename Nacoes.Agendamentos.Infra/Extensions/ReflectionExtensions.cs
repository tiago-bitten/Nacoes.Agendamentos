﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Infra.Extensions;

public static class ReflectionExtensions
{
    public static bool IsEntityId(this Type type)
        => type.BaseType?.IsGenericType == true && type.BaseType.GetGenericTypeDefinition() == typeof(EntityId<>);
}
