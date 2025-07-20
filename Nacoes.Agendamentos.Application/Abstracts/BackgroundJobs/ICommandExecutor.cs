using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;

public interface ICommandExecutor
{
    Task ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
    Task<Result<TResponse>> ExecuteCommandAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse>;
}