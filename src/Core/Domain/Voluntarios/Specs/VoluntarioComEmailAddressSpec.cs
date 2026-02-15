using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Voluntarios.Specs;

public sealed class VolunteerWithEmailAddressSpec(string emailAddress) : Specification<Volunteer>
{
    public override Expression<Func<Volunteer, bool>> ToExpression()
        => x => x.Email != null && x.Email.Address == emailAddress;
}
