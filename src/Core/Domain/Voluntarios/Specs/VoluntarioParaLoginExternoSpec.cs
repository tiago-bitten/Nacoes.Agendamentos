using System.Linq.Expressions;
using Domain.Shared.Specifications;
using Domain.Shared.ValueObjects;

namespace Domain.Voluntarios.Specs;

public sealed class VoluntarioParaLoginExternoSpec(DataNascimento dataNascimento, CPF cpf) : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => x => x.DataNascimento != null &&
                x.Cpf != null
                && x.DataNascimento.Valor == dataNascimento.Valor
                && x.Cpf == cpf;
}
