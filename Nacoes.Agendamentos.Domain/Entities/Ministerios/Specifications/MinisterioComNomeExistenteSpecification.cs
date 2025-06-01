using Nacoes.Agendamentos.Domain.Abstracts;
using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios.Specifications;

public sealed class MinisterioComNomeExistenteSpecification(string nome) : Specification<Ministerio>
{
    public override Expression<Func<Ministerio, bool>> ToExpression()
    {
        return ministerio => ministerio.Nome == nome;
    }
}
