using Domain.Shared.Results;

namespace Domain.Usuarios;

public static class UsuarioErrors
{
    public static readonly Error SenhaCurta =
        Error.Problem("Usuarios.SenhaCurta", "A senha deve ter no mínimo 4 caracteres.");

    public static readonly Error NomeObrigatorio =
        Error.Problem("Usuarios.NomeObrigatorio", "O nome do usuário é obrigatório.");

    public static readonly Error SenhaNaoNecessaria =
        Error.Problem("Usuarios.SenhaNaoNecessaria", "Autenticação difeente de conta Nações não precisa informar senha.");

    public static readonly Error EmailEmUso =
        Error.Conflict("Usuarios.EmailEmUso", "O email informado já esta em uso.");

    public static readonly Error NaoEncontrado =
        Error.NotFound("Usuarios.NaoEncontrado", "Não foi possivel encontrar o usuário.");

    public static readonly Error AutenticacaoInvalida =
        Error.Problem("Usuarios.AutenticacaoInvalida", "Autenticação inválida.");

    public static readonly Error SenhaInvalida =
        Error.Problem("Usuarios.SenhaInvalida", "Senha inválida.");

    public static readonly Error MinisteriosObrigatorio =
        Error.Problem("Usuarios.MinisteriosObrigatorio", "O usuário deve estar vinculado a pelo menos um ministerio.");

    public static readonly Error MinisterioNaoVinculadoAoUsuario =
        Error.Problem("Usuarios.MinisterioNaoVinculadoAoUsuario", "O ministerio informado não está vinculado ao usuario.");

    public static readonly Error MinisterioJaVinculadoAoUsuario =
        Error.Problem("Usuarios.MinisterioJaVinculadoAoUsuario", "O ministerio informado já está vinculado ao usuario.");
}
