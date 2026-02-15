using System.Linq.Expressions;
using Domain.Shared.Specifications;
using Domain.Shared.ValueObjects;

namespace Domain.Voluntarios.Specs;

public sealed class VolunteerForExternalLoginSpec(BirthDate birthDate, CPF cpf) : Specification<Volunteer>
{
    public override Expression<Func<Volunteer, bool>> ToExpression()
        => x => x.BirthDate != null &&
                x.Cpf != null
                && x.BirthDate.Value == birthDate.Value
                && x.Cpf == cpf;
}
