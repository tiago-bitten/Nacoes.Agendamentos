using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Usuarios.Specs;

public sealed class ConvitesPendentesSpec : Specification<UsuarioConvite>
{
    public override Expression<Func<UsuarioConvite, bool>> ToExpression()
        => x => x.Status == EConviteStatus.Pendente;
}
