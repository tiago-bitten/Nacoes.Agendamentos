using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Infra.Abstracts;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity<T>, IAggregateRoot
{
    #region Ctor
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _entities;

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<T>();
    }
    #endregion

    #region AddAsync
    public async Task AddAsync(T entidade)
    {
        await _entities.AddAsync(entidade);
    }
    #endregion

    #region DeleteAsync
    public async Task DeleteAsync(T entidade)
    {
        await Task.FromResult(_entities.Remove(entidade));
    }
    #endregion

    #region GetAll
    public IQueryable<T> GetAll(params string[]? includes)
    {
        IQueryable<T> query = _entities.AsQueryable();

        if (includes != null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        return query;
    }
    #endregion

    #region GetByIdAsync
    public async Task<T?> GetByIdAsync(Id<T> id, bool asNoTracking = false, params string[]? includes)
    {
        IQueryable<T> query = _entities;

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }
    #endregion

    #region GetByIdToProject
    public IQueryable<T> GetByIdToProject(Id<T> id)
    {
        var query = _entities.AsNoTracking();
        return query.Where(e => e.Id == id);
    }
    #endregion

    #region UpdateAsync
    public async Task UpdateAsync(T entidade)
    {
        await Task.FromResult(_entities.Update(entidade));
    }
    #endregion
}
