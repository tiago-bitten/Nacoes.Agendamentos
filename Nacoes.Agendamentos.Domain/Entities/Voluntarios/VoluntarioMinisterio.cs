using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Common;
using MinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Ministerio>;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public sealed class VoluntarioMinisterio : EntityId<VoluntarioMinisterio>
{
    private VoluntarioMinisterio() { }

    internal VoluntarioMinisterio(MinisterioId ministerioId)
    {
        MinisterioId = ministerioId;
        Status = EVoluntarioMinisterioStatus.Ativo;
    }

    public MinisterioId MinisterioId { get; private set; } = null!;
    public EVoluntarioMinisterioStatus Status { get; private set; }

    #region Ativar
    public Result Ativar()
    {
        if (Status is EVoluntarioMinisterioStatus.Ativo)
        {
            return VoluntarioMinisterioErrors.VoluntarioJaEstaAtivo;
        }

        Status = EVoluntarioMinisterioStatus.Ativo;
        
        return Result.Success();
    }
    #endregion

    #region Suspender
    public Result Suspender()
    {
        if (Status is EVoluntarioMinisterioStatus.Suspenso)
        {
            return VoluntarioMinisterioErrors.VoluntarioJaEstaSuspenso;
        }

        Status = EVoluntarioMinisterioStatus.Suspenso;
        
        return Result.Success();
    }
    #endregion
}

public enum EVoluntarioMinisterioStatus
{
    Ativo = 0,
    Suspenso = 1,
}
