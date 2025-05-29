using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Application.Abstracts;
public abstract class BaseHandler(IUnitOfWork uow)
{
    protected IUnitOfWork Uow => uow;

}
