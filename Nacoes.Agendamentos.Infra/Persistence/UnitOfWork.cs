using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Persistence;

public class UnitOfWork : IUnitOfWork
{
    #region Constructor
    private readonly NacoesDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(NacoesDbContext context)
    {
        _context = context;
    }
    #endregion

    #region BeginAsync
    public async Task BeginAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
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
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
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
        _context.Dispose();
        _transaction?.Dispose();
    }
    #endregion
}