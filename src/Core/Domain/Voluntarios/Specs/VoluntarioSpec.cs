using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Voluntarios.Specs;

public sealed class VolunteerSpec : Specification<Volunteer>
{
    public override Expression<Func<Volunteer, bool>> ToExpression()
        => x => x.Ministries.Any();
}
