using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;

public static class VoluntarioErrors
{
    public static readonly Error NaoEncontrado = 
        Error.NotFound("Voluntario.NaoEncontrado", "Voluntario não encontrado.");
    
    public static readonly Error NomeObrigatorio = 
        Error.Problem("Voluntario.NomeObrigatorio", "O nome do voluntário é obrigatório.");
    
    public static Error DadosPessoaisObrigatorio(string dados) =>
        Error.Problem("Voluntario.DadosPessoaisObrigatorio", $"Os seguintes dados pessoais são obrigatórios: {dados}");
    
    public static readonly Error AutenticacaoInvalida = 
        Error.Problem("Voluntario.AutenticacaoInvalida", "É necessário informar o CPF e a data de nascimento.");
    
    public static readonly Error VoluntarioLoginNaoEncontrado = 
        Error.NotFound("Voluntario.LoginNaoEncontrado", "Não foi possível localizar seu cadastro.");
}