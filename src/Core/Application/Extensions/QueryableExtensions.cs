using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate)
        => condition ? query.Where(predicate) : query;

    public static IQueryable<T> ApplySpec<T>(this IQueryable<T> query, ISpecification<T> spec)
        => query.Where(spec.ToExpression());
}
