using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Usuarios.Specs;

public sealed class UsuarioComEmailAddressSpec(string emailAddress) : Specification<Usuario>
{
    public override Expression<Func<Usuario, bool>> ToExpression()
        => x => x.Email.Address == emailAddress;
}
