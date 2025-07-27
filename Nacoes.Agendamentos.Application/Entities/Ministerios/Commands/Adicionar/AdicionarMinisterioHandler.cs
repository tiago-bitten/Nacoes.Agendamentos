using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Mappings;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.Adicionar;

internal sealed class AdicionarMinisterioHandler(IUnitOfWork uow,
                                                 IMinisterioRepository ministerioRepository)
    : ICommandHandler<AdicionarMinisterioCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AdicionarMinisterioCommand command, CancellationToken cancellationToken = default)
    {
        var ministerio = command.GetEntidade();

        /*var nomeExistente = await GetSpecification(new MinisterioComNomeExistenteSpecification(ministerio.Nome),
                                                   ministerioRepository);
        if (nomeExistente)
        {
            return MinisterioErrors.MinisterioComNomeExistente;
        }*/

        await uow.BeginAsync();
        await ministerioRepository.AddAsync(ministerio);
        await uow.CommitAsync();

        return Result<Guid>.Success(ministerio.Id);
    }
}
