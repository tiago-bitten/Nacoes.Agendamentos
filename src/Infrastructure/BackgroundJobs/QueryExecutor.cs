using Application.Shared.Ports.BackgroundJobs;
using Application.Shared.Messaging;
using Domain.Shared.Results;

namespace Infrastructure.BackgroundJobs;

internal sealed class QueryExecutor(IServiceProvider serviceProvider) : IQueryExecutor
{
    public async Task<Result<TResponse>> ExecuteQueryAsync<TQuery, TResponse>(
        TQuery command,
        CancellationToken ct = default)
        where TQuery : IQuery<TResponse>
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(typeof(TQuery), typeof(TResponse));

        if (serviceProvider.GetService(handlerType) is not IQueryHandler<TQuery, TResponse> handler)
        {
            throw new InvalidOperationException($"No handler found for command {typeof(TQuery).Name} with response {typeof(TResponse).Name}.");
        }

        return await handler.Handle(command, ct);
    }
}
