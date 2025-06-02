using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;
using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios.Specifications;

public sealed class AtividadeComNomeExistenteSpecification(string nome, Id<Ministerio> ministerioId) 
    : Specification<Ministerio>
{
    public override Expression<Func<Ministerio, bool>> ToExpression()
        => ministerio => ministerio.Id.Value == ministerioId.Value 
                         && ministerio.Atividades.Any(atividade => atividade.Nome == nome);
}
