using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Abstracts.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
