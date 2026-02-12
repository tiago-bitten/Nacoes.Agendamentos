using Application.Shared.Messaging;
using Domain.Shared.Results;
using System.Transactions;

namespace Application.Shared.Behaviors;

internal static class TransactionDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler
    ) : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var result = await innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
            {
                scope.Complete();
            }

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler
    ) : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var result = await innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
            {
                scope.Complete();
            }

            return result;
        }
    }
}
