using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Voluntarios.Specs;

public sealed class VoluntarioSpec : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => x => x.Ministerios.Any();
}
