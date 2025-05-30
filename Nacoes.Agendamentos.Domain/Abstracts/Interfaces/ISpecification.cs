using System.Linq.Expressions;

namespace Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> ToExpression();
}