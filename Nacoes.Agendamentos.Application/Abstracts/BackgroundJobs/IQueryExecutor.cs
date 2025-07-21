using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;

public interface IQueryExecutor
{
    Task<Result<TResponse>> ExecuteQueryAsync<TQuery, TResponse>(TQuery command) where TQuery : IQuery<TResponse>;
}