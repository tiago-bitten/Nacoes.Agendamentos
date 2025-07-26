using Microsoft.EntityFrameworkCore.Storage;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Persistence;

internal class UnitOfWork(NacoesDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    #region BeginAsync
    public async Task BeginAsync()
    {
        _transaction = await context.Database.BeginTransactionAsync();
    }
    #endregion

    #region CommitAsync
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction started.");
        }
        try
        {
            var domainEvents = context.GetDomainEvents();
            
            await context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);

            await context.PublishDomainEventsAsync(domainEvents);
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    
    public async Task CommitAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        await BeginAsync();
        await action();
        await CommitAsync(cancellationToken);
    }
    #endregion

    #region RollbackAsync
    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    #endregion

    #region Dispose
    public void Dispose()
    {
        context.Dispose();
        _transaction?.Dispose();
    }
    #endregion
    
    #region HasActiveTransaction
    public bool HasActiveTransaction => _transaction != null;
    #endregion
}