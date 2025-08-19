using System.Linq.Expressions;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specs;

public sealed class VoluntarioComEmailAddressSpec(string emailAddress) : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => x => x.Email != null && x.Email.Address == emailAddress;
}