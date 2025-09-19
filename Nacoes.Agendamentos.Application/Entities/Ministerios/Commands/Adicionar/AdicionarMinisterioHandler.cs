using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Mappings;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.Adicionar;

internal sealed class AdicionarMinisterioHandler(
    INacoesDbContext context)
    : ICommandHandler<AdicionarMinisterioCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarMinisterioCommand command, CancellationToken cancellationToken = default)
    {
        var ministerio = command.GetEntidade();
        
        await context.Ministerios.AddAsync(ministerio, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ministerio.Id;
    }
}
