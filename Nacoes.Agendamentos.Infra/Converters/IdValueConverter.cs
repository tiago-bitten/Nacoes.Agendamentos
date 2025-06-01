using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Infra.Converters;
public class IdValueConverter<T> : ValueConverter<Id<T>, Guid>
{
    public IdValueConverter() : base(
        id => id.Value,          
        guid => new Id<T>(guid.ToString())) 
    {
    }
}
