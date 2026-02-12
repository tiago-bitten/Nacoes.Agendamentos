using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Voluntarios.Specs;

public class VoluntarioPorVinculoMinisterioSpec(Guid voluntarioMinisterioId) : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => voluntario => voluntario.Ministerios.Any(x => x.Id == voluntarioMinisterioId);
}
