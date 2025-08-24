using System.Linq.Expressions;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specs;

public class VoluntarioPorVinculoMinisterioSpec(Guid voluntarioMinisterioId) : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => voluntario => voluntario.Ministerios.Any(x => x.Id == voluntarioMinisterioId);
}