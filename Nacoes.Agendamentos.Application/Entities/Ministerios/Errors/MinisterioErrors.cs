using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Errors;

public static class MinisterioErrors
{
    public static readonly Error MinisterioComNomeExistente =
        new("Ministerio.MinisterioComNomeExistente", ErrorType.Validation, "O ministerio com o nome informado ja existe.");
}
