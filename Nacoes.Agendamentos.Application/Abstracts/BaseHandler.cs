using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Application.Abstracts;
public abstract class BaseHandler(IUnitOfWork uow)
{
    protected IUnitOfWork Uow => uow;

    protected Task<bool> GetSpecification<T>(ISpecification<T> spec, IBaseRepository<T> repository) where T : EntityId<T>
        => repository.FindAsync(spec);
}
