using System.Linq.Expressions;

namespace Domain.Shared.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public bool IsSatisfiedBy(T entity)
        => ToExpression().Compile().Invoke(entity);

    public Specification<T> And(Specification<T> specification)
        => new AndSpecification<T>(this, specification);

    public Specification<T> Or(Specification<T> specification)
        => new OrSpecification<T>(this, specification);

    public Specification<T> Not()
        => new NotSpecification<T>(this);
}

internal class AndSpecification<T>(
    Specification<T> left,
    Specification<T> right)
    : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = left.ToExpression();
        var rightExpr = right.ToExpression();

        var param = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(
            Expression.Invoke(leftExpr, param),
            Expression.Invoke(rightExpr, param));

        return Expression.Lambda<Func<T, bool>>(body, param);
    }
}

internal class OrSpecification<T>(
    Specification<T> left,
    Specification<T> right)
    : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = left.ToExpression();
        var rightExpr = right.ToExpression();

        var param = Expression.Parameter(typeof(T));
        var body = Expression.OrElse(
            Expression.Invoke(leftExpr, param),
            Expression.Invoke(rightExpr, param));

        return Expression.Lambda<Func<T, bool>>(body, param);
    }
}

internal class NotSpecification<T>(Specification<T> spec) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var expr = spec.ToExpression();
        var param = expr.Parameters[0];
        var body = Expression.Not(expr.Body);

        return Expression.Lambda<Func<T, bool>>(body, param);
    }
}
