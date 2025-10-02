using System.Linq.Expressions;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specs;

public sealed class VoluntarioParaLoginExternoSpec(DataNascimento dataNascimento, CPF cpf) : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => x => x.DataNascimento != null && 
                x.Cpf != null
                && x.DataNascimento.Valor == dataNascimento.Valor 
                && x.Cpf == cpf;
}