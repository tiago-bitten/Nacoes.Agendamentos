using System.Linq.Expressions;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate)
        => condition ? query.Where(predicate) : query;
    
    public static IQueryable<T> WhereSpec<T>(this IQueryable<T> query, ISpecification<T> spec)
        => query.Where(spec.ToExpression());
}