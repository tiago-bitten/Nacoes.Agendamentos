using Nacoes.Agendamentos.Domain.Entities.Voluntarios;

namespace Nacoes.Agendamentos.Application.Reports.Queries.InfoDiariaUsoApp;

public record RecuperarInfoDiariaUsoAppResponse
{
    public required DateTimeOffset Data { get; init; }
    public required VoluntarioInfo Voluntarios { get; init; }

    public record VoluntarioInfo
    {
        public required List<VoluntarioInfoOrigem> Origens { get; init; } = [];
        public required int QuantidadeTotal { get; init; }
        
        public record VoluntarioInfoOrigem
        {
            public required EOrigemCadastroVoluntario Origem { get; init; }
            public required int Quantidade { get; init; }
        }
    }
}