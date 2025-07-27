using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;
using MinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Ministerio>;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public sealed class VoluntarioMinisterio : EntityId<VoluntarioMinisterio>
{
    private VoluntarioMinisterio() { }

    internal VoluntarioMinisterio(MinisterioId ministerioId)
    {
        MinisterioId = ministerioId;
        Status = EVoluntarioMinisterioStatus.Vinculado;
    }

    public MinisterioId MinisterioId { get; private set; } = null!;
    public EVoluntarioMinisterioStatus Status { get; private set; }
    
    public Ministerio Ministerio { get; private set; } = null!;
    public Voluntario Voluntario { get; private set; } = null!;

    #region Vincular
    public Result Vincular()
    {
        if (Status is EVoluntarioMinisterioStatus.Vinculado)
        {
            return VoluntarioMinisterioErrors.VoluntarioJaEstaVinculado;
        }

        Status = EVoluntarioMinisterioStatus.Vinculado;
        
        return Result.Success();
    }
    #endregion
    
    #region Desvincular
    public Result Desvincular()
    {
        if (Status is EVoluntarioMinisterioStatus.Desvinculado)
        {
            return VoluntarioMinisterioErrors.VoluntarioJaEstaDesvinculado;
        }

        Status = EVoluntarioMinisterioStatus.Desvinculado;
        
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
    Vinculado = 0,
    Desvinculado = 1,
    Suspenso = 2,
}
