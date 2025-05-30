using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios;

public sealed class VoluntarioMinisterio : EntityId<VoluntarioMinisterio>, IAggregateRoot
{
    #region Constructors
    internal VoluntarioMinisterio() { }

    public VoluntarioMinisterio(Id<Voluntario> voluntarioId, Id<Ministerio> ministerioId)
    {
        VoluntarioId = voluntarioId;
        MinisterioId = ministerioId;
        Status = EVoluntarioMinisterioStatus.Ativo;
    }
    #endregion

    public Id<Voluntario> VoluntarioId { get; private set; }
    public Id<Ministerio> MinisterioId { get; private set; }
    public EVoluntarioMinisterioStatus Status { get; private set; }

    #region Ativar
    public void Ativar()
    {
        if (Status == EVoluntarioMinisterioStatus.Ativo)
        {
            Throw.VoluntarioJaEstaAtivo();
        }

        Status = EVoluntarioMinisterioStatus.Ativo;
    }
    #endregion

    #region Suspender
    public void Suspender()
    {
        if (Status == EVoluntarioMinisterioStatus.Suspenso)
        {
            Throw.VoluntarioJaEstaSuspenso();
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
