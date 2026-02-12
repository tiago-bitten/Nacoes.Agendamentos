using Application.Shared.Messaging;
using Domain.Shared.Results;

namespace Application.Shared.Ports.BackgroundJobs;

public interface IQueryExecutor
{
    Task<Result<TResponse>> ExecuteQueryAsync<TQuery, TResponse>(TQuery command) where TQuery : IQuery<TResponse>;
}
