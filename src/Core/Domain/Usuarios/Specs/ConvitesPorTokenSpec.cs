using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Usuarios.Specs;

public sealed class ConvitesPorTokenSpec(string token) : Specification<UsuarioConvite>
{
    public override Expression<Func<UsuarioConvite, bool>> ToExpression()
        => x => x.Token == token;
}
