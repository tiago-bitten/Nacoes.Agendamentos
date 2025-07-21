using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Reports.Queries.InfoDiariaUsoApp;

public sealed record RecuperarInfoDiariaUsoAppQuery : IQuery<RecuperarInfoDiariaUsoAppResponse>
{
    public bool EnviarPorEmail { get; init; }
}