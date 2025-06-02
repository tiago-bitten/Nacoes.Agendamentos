using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;
using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specifications;

public sealed class VoluntarioComEmailExistenteSpecification(Email email) : Specification<Voluntario>
{
    public override Expression<Func<Voluntario, bool>> ToExpression()
        => voluntario => voluntario.Email!.Address == email.Address;
}
