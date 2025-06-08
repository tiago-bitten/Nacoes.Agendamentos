using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios;

public sealed class VoluntarioMinisterio : EntityId<VoluntarioMinisterio>
{
    #region Constructors
    internal VoluntarioMinisterio() { }

    internal VoluntarioMinisterio(Id<Ministerio> ministerioId)
    {
        MinisterioId = ministerioId;
        Status = EVoluntarioMinisterioStatus.Ativo;
    }
    #endregion

    public Id<Ministerio> MinisterioId { get; private set; }
    public EVoluntarioMinisterioStatus Status { get; private set; }

    #region Ativar
    public void Ativar()
    {
        if (Status == EVoluntarioMinisterioStatus.Ativo)
        {
            throw ExceptionFactory.VoluntarioJaEstaAtivo();
        }

        Status = EVoluntarioMinisterioStatus.Ativo;
    }
    #endregion

    #region Suspender
    public void Suspender()
    {
        if (Status == EVoluntarioMinisterioStatus.Suspenso)
        {
            throw ExceptionFactory.VoluntarioJaEstaSuspenso();
        }

        Status = EVoluntarioMinisterioStatus.Suspenso;
    }
    #endregion
}

public enum EVoluntarioMinisterioStatus
{
    Ativo,
    Suspenso,
}
