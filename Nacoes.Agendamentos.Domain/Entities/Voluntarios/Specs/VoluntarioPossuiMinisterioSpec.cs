using System.Linq.Expressions;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specs;

public sealed class VoluntarioSpec : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => x => x.Ministerios.Any();
}