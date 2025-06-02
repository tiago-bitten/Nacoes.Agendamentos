using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;
using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specifications;

public sealed class VoluntarioComCpfExistenteSpecification(CPF cpf) : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => voluntario => voluntario.Cpf!.Numero == cpf.Numero;
}
