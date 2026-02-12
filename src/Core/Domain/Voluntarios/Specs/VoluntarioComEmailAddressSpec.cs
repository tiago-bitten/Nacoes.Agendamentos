using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Voluntarios.Specs;

public sealed class VoluntarioComEmailAddressSpec(string emailAddress) : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => x => x.Email != null && x.Email.Address == emailAddress;
}
