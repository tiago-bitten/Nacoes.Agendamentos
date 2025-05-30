using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;
using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.Specifications;

public sealed class UsuarioComEmailExistenteSpecification(Email email) : Specification<Usuario>
{
    public override Expression<Func<Usuario, bool>> ToExpression() 
        => u => u.Email == email;
}
