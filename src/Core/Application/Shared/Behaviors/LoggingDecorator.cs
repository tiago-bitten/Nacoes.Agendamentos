using Application.Shared.Messaging;
using Domain.Shared.Results;
using Microsoft.Extensions.Logging;

namespace Application.Shared.Behaviors;

internal static class LoggingDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        ILogger<CommandHandler<TCommand, TResponse>> logger
    ) : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            var commandName = typeof(TCommand).Name;

            logger.LogInformation("Executing command {CommandName}...", commandName);

            var result = await innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation("Command {CommandName} executed successfully.", commandName);
            }
            else
            {
                logger.LogWarning("Command {CommandName} failed with error: {Error}", commandName, result.Error);
            }

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        ILogger<CommandBaseHandler<TCommand>> logger
    ) : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            var commandName = typeof(TCommand).Name;

            logger.LogInformation("Executing command {CommandName}...", commandName);

            var result = await innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation("Command {CommandName} executed successfully.", commandName);
            }
            else
            {
                logger.LogWarning("Command {CommandName} failed with error: {Error}", commandName, result.Error);
            }

            return result;
        }
    }
}
