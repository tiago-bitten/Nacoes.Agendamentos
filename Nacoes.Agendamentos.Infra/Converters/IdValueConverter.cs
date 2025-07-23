using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Infra.Converters;
internal sealed class IdValueConverter<T>() 
    : ValueConverter<Id<T>, Guid>(id => id.Value, guid => new Id<T>(guid.ToString()));
