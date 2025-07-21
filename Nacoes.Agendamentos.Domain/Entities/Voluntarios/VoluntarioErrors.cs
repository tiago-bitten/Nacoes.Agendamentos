using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public static class VoluntarioErrors
{
    public static readonly Error NaoEncontrado = 
        new("Voluntario.NaoEncontrado", "Voluntario não encontrado.");
    
    public static readonly Error NomeObrigatorio = 
        new("Voluntario.NomeObrigatorio", "O nome do voluntário é obrigatório.");
    
    public static Error DadosPessoaisObrigatorio(string dados) =>
        new("Voluntario.DadosPessoaisObrigatorio", $"Os seguintes dados pessoais são obrigatórios: {dados}");
}