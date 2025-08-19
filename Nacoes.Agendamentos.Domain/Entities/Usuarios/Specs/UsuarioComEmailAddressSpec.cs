using System.Linq.Expressions;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.Specs;

public sealed class UsuarioComEmailAddressSpec(string emailAddress) : Specification<Usuario>
{
    public override Expression<Func<Usuario, bool>> ToExpression()
        => x => x.Email.Address == emailAddress;
}