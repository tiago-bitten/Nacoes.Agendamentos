using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Abstracts;

internal abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityId<T>
{
    #region Ctor
    private readonly NacoesDbContext _dbContext;
    private readonly DbSet<T> _entities;

    protected DbSet<TEntidade> Escolher<TEntidade>() where TEntidade : EntityId<TEntidade>
        => _dbContext.Set<TEntidade>();

    public BaseRepository(NacoesDbContext dbContext)
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

        if (includes is null)
        {
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }
        
        query = includes.Aggregate(query, (current, include) => current.Include(include));

        query = query.AsSplitQuery();

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
    
    #region GetOnlyIdAsync

    public Task<Id<T>?> GetOnlyIdAsync(Id<T> id)
    {
        return GetByIdToProject(id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }

    #endregion

    #region UpdateAsync
    public async Task UpdateAsync(T entidade)
    {
        await Task.FromResult(_entities.Update(entidade));
    }
    #endregion

    #region FindAsync
    public Task<bool> FindAsync(ISpecification<T> spec)
    {
        return _entities.AnyAsync(spec.ToExpression());
    }
    #endregion
}
