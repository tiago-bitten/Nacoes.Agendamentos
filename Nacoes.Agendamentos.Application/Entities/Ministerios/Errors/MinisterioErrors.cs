using Nacoes.Agendamentos.Application.Common.Results;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Errors;

public static class MinisterioErrors
{
    public static readonly Error MinisterioComNomeExistente =
        new("Ministerio.Adicionar.EmailExistente", "Já existe um ministerio com o nome informado.");
}
