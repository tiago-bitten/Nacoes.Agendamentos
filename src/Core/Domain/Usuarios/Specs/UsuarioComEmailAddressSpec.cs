using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Usuarios.Specs;

public sealed class UserWithEmailAddressSpec(string emailAddress) : Specification<User>
{
    public override Expression<Func<User, bool>> ToExpression()
        => x => x.Email.Address == emailAddress;
}
