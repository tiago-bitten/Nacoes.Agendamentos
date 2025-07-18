using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public static class UsuarioErrors
{
    public static readonly Error SenhaCurta = 
        new("Usuarios.SenhaCurta", "A senha deve ter no mínimo 4 caracteres.");
    
    public static readonly Error NomeObrigatorio = 
        new("Usuarios.NomeObrigatorio", "O nome do usuário é obrigatório.");
    
    public static readonly Error SenhaNaoNecessaria = 
        new("Usuarios.SenhaNaoNecessaria", "Autenticação difeente de conta Nações não precisa informar senha.");
}