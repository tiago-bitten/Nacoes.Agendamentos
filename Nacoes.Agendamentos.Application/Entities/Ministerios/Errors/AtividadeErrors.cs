using Nacoes.Agendamentos.Application.Common.Results;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Errors;

public static class AtividadeErrors
{
    public readonly static Error AtividadeComNomeExistente
        = new("Miniserio.Atividade.Adicionar", "Já existe uma atividade com esse nome para o ministério informado.");
}
