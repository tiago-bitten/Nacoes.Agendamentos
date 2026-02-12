using Application.Shared.Messaging;
using Domain.Shared.Results;

namespace Application.Shared.Ports.BackgroundJobs;

public interface ICommandExecutor
{
    Task ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
    Task<Result<TResponse>> ExecuteCommandAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse>;
}
