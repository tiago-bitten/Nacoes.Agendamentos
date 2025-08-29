using System.Linq.Expressions;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.Specs;

public sealed class ConvitesPorTokenSpec(string token) : Specification<UsuarioConvite>
{
    public override Expression<Func<UsuarioConvite, bool>> ToExpression()
        => x => x.Token == token;
}