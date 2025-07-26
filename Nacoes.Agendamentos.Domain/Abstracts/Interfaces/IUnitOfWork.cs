namespace Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task BeginAsync();
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(Func<Task> action, CancellationToken cancellationToken = default);
    Task RollbackAsync();
    bool HasActiveTransaction { get; }
}
