﻿using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios;

public static class AtividadeErrors
{
    
    public static readonly Error NomeObrigatorio =
        Error.Problem("Atividade.NomeObrigatorio", "O nome da atividade é obrigatório.");
    
    public static readonly Error NomeEmUso =
        Error.Problem("Atividade.NomeEmUso", "Já existe uma atividade com esse nome.");
}