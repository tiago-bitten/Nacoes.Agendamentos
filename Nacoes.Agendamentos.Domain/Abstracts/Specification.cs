using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Domain.Abstracts;

public abstract class Specification<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }
}
