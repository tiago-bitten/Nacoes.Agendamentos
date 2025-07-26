using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Mappings;
public static class MinisterioMapper
{
    public static Ministerio GetEntidade(this AdicionarMinisterioCommand command)
        => new(command.Nome,
               command.Descricao,
               command.Cor == null ? null : new Cor(command.Cor.Valor, command.Cor.Tipo));
}
