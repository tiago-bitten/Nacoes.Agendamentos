using Application.Shared.Messaging;
using Domain.Shared.Results;

namespace Application.Shared.Ports.BackgroundJobs;

public interface ICommandExecutor
{
    Task ExecuteCommandAsync<TCommand>(TCommand command, CancellationToken ct = default) where TCommand : ICommand;
    Task<Result<TResponse>> ExecuteCommandAsync<TCommand, TResponse>(TCommand command, CancellationToken ct = default) where TCommand : ICommand<TResponse>;
}
