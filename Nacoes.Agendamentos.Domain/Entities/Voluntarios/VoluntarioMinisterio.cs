using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Exceptions;
using MinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Ministerio>;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public sealed class VoluntarioMinisterio : EntityId<VoluntarioMinisterio>
{
    #region Construtores
    private VoluntarioMinisterio() { }

    internal VoluntarioMinisterio(MinisterioId ministerioId)
    {
        MinisterioId = ministerioId;
        Status = EVoluntarioMinisterioStatus.Ativo;
    }
    #endregion

    public MinisterioId MinisterioId { get; private set; }
    public EVoluntarioMinisterioStatus Status { get; private set; }

    #region Ativar
    public void Ativar()
    {
        if (Status is EVoluntarioMinisterioStatus.Ativo)
        {
            throw ExceptionFactory.VoluntarioJaEstaAtivo();
        }

        Status = EVoluntarioMinisterioStatus.Ativo;
    }
    #endregion

    #region Suspender
    public void Suspender()
    {
        if (Status is EVoluntarioMinisterioStatus.Suspenso)
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
