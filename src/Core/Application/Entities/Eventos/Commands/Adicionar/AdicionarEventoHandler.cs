using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;
using Domain.Eventos;
using Domain.Eventos.DomainEvents;
using Domain.Shared.ValueObjects;

namespace Application.Entities.Eventos.Commands.Adicionar;

internal sealed class AdicionarEventoHandler(INacoesDbContext context)
    : ICommandHandler<AdicionarEventoCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarEventoCommand command, CancellationToken cancellation = default)
    {
        var eventoResult = Evento.Criar(
            command.Descricao,
            new Horario(command.Horario.DataInicial, command.Horario.DataFinal),
            new RecorrenciaEvento(command.Recorrencia.Tipo, command.Recorrencia.Intervalo, command.Recorrencia.DataFinal),
            command.QuantidadeMaximaReservas);

        if (eventoResult.IsFailure)
        {
            return eventoResult.Error;
        }

        var evento = eventoResult.Value;

        await context.Eventos.AddAsync(evento, cancellation);

        evento.Raise(new EventoMasterAdicionadoDomainEvent(evento.Id));
        await context.SaveChangesAsync(cancellation);

        return evento.Id;
    }
}
