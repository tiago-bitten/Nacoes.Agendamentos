using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;

public static class VoluntarioErrors
{
    public static readonly Error NaoEncontrado = 
        new("Voluntario.NaoEncontrado", ErrorType.NotFound, "Voluntario não encontrado.");
    
    public static readonly Error NomeObrigatorio = 
        new("Voluntario.NomeObrigatorio", ErrorType.Validation, "O nome do voluntário é obrigatório.");
    
    public static Error DadosPessoaisObrigatorio(string dados) =>
        new("Voluntario.DadosPessoaisObrigatorio", ErrorType.Validation, $"Os seguintes dados pessoais são obrigatórios: {dados}");
}