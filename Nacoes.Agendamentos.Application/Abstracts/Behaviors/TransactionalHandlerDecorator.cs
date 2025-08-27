using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Abstracts.Behaviors;

internal static class TransactionDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        DbContext context
    ) : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            if (context.Database.CurrentTransaction is not null)
            {
                return await innerHandler.Handle(command, cancellationToken);
            }

            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var result = await innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                await transaction.CommitAsync(cancellationToken);
            }

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        DbContext context
    ) : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            if (context.Database.CurrentTransaction is not null)
            {
                return await innerHandler.Handle(command, cancellationToken);
            }

            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var result = await innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                await transaction.CommitAsync(cancellationToken);
            }

            return result;
        }
    }
}
