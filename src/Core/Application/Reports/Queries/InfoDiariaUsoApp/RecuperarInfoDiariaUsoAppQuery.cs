using Application.Shared.Messaging;

namespace Application.Reports.Queries.InfoDiariaUsoApp;

public sealed record RecuperarInfoDiariaUsoAppQuery : IQuery<RecuperarInfoDiariaUsoAppResponse>
{
    public bool EnviarPorEmail { get; init; }
}
