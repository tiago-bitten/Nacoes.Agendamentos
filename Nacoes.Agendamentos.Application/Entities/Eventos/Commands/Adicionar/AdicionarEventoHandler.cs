using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Eventos;
using Nacoes.Agendamentos.Domain.Entities.Eventos.DomainEvents;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.Adicionar;

internal sealed class AdicionarEventoHandler(INacoesDbContext context)
    : ICommandHandler<AdicionarEventoCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarEventoCommand command, CancellationToken cancellation = default)
    {
        var eventoResult = Evento.Criar(
            command.Descricao,
            new Horario(command.Horario.DataInicial, command.Horario.DataFinal),
            new RecorrenciaEvento(command.Recorrencia.Tipo, command.Recorrencia.Valor, command.Recorrencia.DataFinal),
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
