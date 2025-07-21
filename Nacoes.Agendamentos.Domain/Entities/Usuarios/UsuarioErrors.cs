using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public static class UsuarioErrors
{
    public static readonly Error SenhaCurta = 
        new("Usuarios.SenhaCurta", ErrorType.Validation, "A senha deve ter no mínimo 4 caracteres.");
    
    public static readonly Error NomeObrigatorio = 
        new("Usuarios.NomeObrigatorio", ErrorType.Validation, "O nome do usuário é obrigatório.");
    
    public static readonly Error SenhaNaoNecessaria = 
        new("Usuarios.SenhaNaoNecessaria", ErrorType.Validation, "Autenticação difeente de conta Nações não precisa informar senha.");
    
    public static readonly Error EmailEmUso = 
        new("Usuarios.EmailEmUso", ErrorType.Validation, "O email informado já esta em uso.");
}