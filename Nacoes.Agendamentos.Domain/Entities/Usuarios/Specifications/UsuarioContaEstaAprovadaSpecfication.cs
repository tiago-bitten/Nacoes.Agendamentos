using Nacoes.Agendamentos.Domain.Abstracts;
using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.Specifications;

public sealed class UsuarioContaEstaAprovadaSpecfication : Specification<Usuario>
{
    public override Expression<Func<Usuario, bool>> ToExpression()
        => usuario => usuario.Solicitacoes.Any(x => x.Status == EStatusAprovacao.Aprovado);
}
