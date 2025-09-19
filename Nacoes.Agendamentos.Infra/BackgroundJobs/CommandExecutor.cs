using Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Infra.BackgroundJobs;

public class CommandExecutor(IServiceProvider serviceProvider) : ICommandExecutor
{
    public async Task ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));

        if (serviceProvider.GetService(handlerType) is not ICommandHandler<TCommand> handler)
        {
            throw new InvalidOperationException($"No handler found for command {typeof(TCommand).Name}.");
        }

        await handler.HandleAsync(command, CancellationToken.None);
    }

    public async Task<Result<TResponse>> ExecuteCommandAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse>
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(typeof(TCommand), typeof(TResponse));

        if (serviceProvider.GetService(handlerType) is not ICommandHandler<TCommand, TResponse> handler)
        {
            throw new InvalidOperationException($"No handler found for command {typeof(TCommand).Name} with response {typeof(TResponse).Name}.");
        }

        return await handler.HandleAsync(command, CancellationToken.None);
    }
}