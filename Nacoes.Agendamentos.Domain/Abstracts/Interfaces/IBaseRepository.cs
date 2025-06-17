using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

public interface IBaseRepository<T> where T : EntityId<T>
{
    Task AddAsync(T entidade);
    Task UpdateAsync(T entidade);
    Task DeleteAsync(T entidade);
    Task<T?> GetByIdAsync(Id<T> id, bool asNoTracking = false, params string[]? includes);
    IQueryable<T> GetAll(params string[]? includes);
    IQueryable<T> GetByIdToProject(Id<T> id);
    Task<Id<T>?> GetOnlyIdAsync(Id<T> id);
    Task<bool> FindAsync(ISpecification<T> spec);
}
