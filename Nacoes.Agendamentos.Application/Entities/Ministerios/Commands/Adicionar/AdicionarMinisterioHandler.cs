using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.Adicionar;

internal sealed class AdicionarMinisterioHandler(
    INacoesDbContext context)
    : ICommandHandler<AdicionarMinisterioCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarMinisterioCommand command, CancellationToken cancellationToken = default)
    {
        var ministerioResult = Ministerio.Criar(
            command.Nome,
            command.Descricao,
            new Cor(command.Cor.Valor, command.Cor.Tipo));
        
        if (ministerioResult.IsFailure)
        {
            return ministerioResult.Error;
        }
        
        var ministerio = ministerioResult.Value;
        
        await context.Ministerios.AddAsync(ministerio, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ministerio.Id;
    }
}
