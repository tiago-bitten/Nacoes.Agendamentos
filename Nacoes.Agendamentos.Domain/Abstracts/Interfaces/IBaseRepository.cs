using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

public interface IBaseRepository<T> where T : EntityId
{
    Task AddAsync(T entidade);
    Task UpdateAsync(T entidade);
    Task DeleteAsync(T entidade);
    Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false, params string[]? includes);
    IQueryable<T> GetAll(params string[]? includes);
    IQueryable<T> GetByIdToProject(Guid id);
    Task<Guid> GetOnlyIdAsync(Guid id);
    Task<bool> FindAsync(ISpecification<T> spec);
}
