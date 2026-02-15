using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Voluntarios.Specs;

public sealed class VolunteerByMinistryLinkSpec(Guid volunteerMinistryId) : Specification<Volunteer>
{
    public override Expression<Func<Volunteer, bool>> ToExpression()
        => volunteer => volunteer.Ministries.Any(x => x.Id == volunteerMinistryId);
}
