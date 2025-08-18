using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Mappings;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.Adicionar;

internal sealed class AdicionarMinisterioHandler(
    INacoesDbContext context, 
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

        await ministerioRepository.AddAsync(ministerio);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(ministerio.Id);
    }
}
