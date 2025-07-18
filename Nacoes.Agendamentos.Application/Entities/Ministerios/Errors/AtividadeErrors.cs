
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Errors;

public static class AtividadeErrors
{
    public static readonly Error AtividadeComNomeExistente
        = new("Atividade.AtividadeComNomeExistente", "Já existe uma atividade com esse nome para o ministério informado.");
}
