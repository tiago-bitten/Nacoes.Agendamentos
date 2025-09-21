﻿using Nacoes.Agendamentos.Application.Common.Pagination;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Queries.Recuperar;

public record VoluntarioResponse : ICursorResponse
{
    public required Guid Id { get; init; }
    public required DateTimeOffset DataCriacao { get; init; }
    public required string Nome { get; init; }
    public List<MinisterioItem> Ministerios { get; init; } = [];
    
    public record MinisterioItem
    {
        public required string Nome { get; init; }
    }
}