using Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Infra.BackgroundJobs;

public class QueryExecutor(IServiceProvider serviceProvider) : IQueryExecutor
{
    public async Task<Result<TResponse>> ExecuteQueryAsync<TQuery, TResponse>(TQuery command) where TQuery : IQuery<TResponse>
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(typeof(TQuery), typeof(TResponse));

        if (serviceProvider.GetService(handlerType) is not IQueryHandler<TQuery, TResponse> handler)
        {
            throw new InvalidOperationException($"No handler found for command {typeof(TQuery).Name} with response {typeof(TResponse).Name}.");
        }

        return await handler.Handle(command, CancellationToken.None);
    }
}