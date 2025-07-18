using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class Usuario : EntityId<Usuario>, IAggregateRoot
{
    private Usuario() { }

    private Usuario(string nome, Email email, EAuthType authType, Celular? celular = null, string? senha = null)
    {
        Nome = nome;
        Email = email;
        Celular = celular;
        AuthType = authType;
        Senha = senha;
    }

    public string Nome { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string? Senha { get; private set; }
    public Celular? Celular { get; private set; }
    public EAuthType AuthType { get; private set; }
    
    #region Criar
    public static Result<Usuario> Criar(string nome, Email email, EAuthType authType, Celular? celular = null, string? senha = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return UsuarioErrors.NomeObrigatorio;
        }

        if (authType is not EAuthType.Local && !string.IsNullOrWhiteSpace(senha))
        {
            return UsuarioErrors.SenhaNaoNecessaria;
        }
        
        var usuario = new Usuario(nome, email, authType, celular, senha);
        return Result<Usuario>.Success(usuario);
    }
    #endregion
    
    #region DefinirSenha
    // TODO: senha deve ser um value object
    public Result DefinirSenha(string senha)
    {
        if (senha.Length < 4)
        {
            return UsuarioErrors.SenhaCurta;
        }

        Senha = senha;
        
        return Result.Success();
    }
    #endregion
}


public enum EAuthType
{
    Local = 0,
    Google = 1,
}