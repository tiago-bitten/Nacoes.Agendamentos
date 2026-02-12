using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Shared.ValueObjects;

namespace Domain.Usuarios;

public sealed class Usuario : RemovableEntity, IAggregateRoot
{
    private readonly List<UsuarioMinisterio> _ministerios = [];

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

    public IReadOnlyList<UsuarioMinisterio> Ministerios => _ministerios.AsReadOnly();

    public static Result<Usuario> Criar(
        string nome,
        Email email,
        EAuthType authType,
        List<Guid> ministeriosIds,
        Celular? celular = null,
        string? senha = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return UsuarioErrors.NomeObrigatorio;
        }

        if (authType is not EAuthType.Local && !string.IsNullOrWhiteSpace(senha))
        {
            return UsuarioErrors.SenhaNaoNecessaria;
        }

        if (ministeriosIds.Count == 0)
        {
            return UsuarioErrors.MinisteriosObrigatorio;
        }

        var usuario = new Usuario(nome, email, authType, celular, senha);

        foreach (var ministerioId in ministeriosIds)
        {
            var usuarioMinisterioVinculoResult = usuario.VincularMinisterio(ministerioId);
            if (usuarioMinisterioVinculoResult.IsFailure)
            {
                return usuarioMinisterioVinculoResult.Error;
            }
        }

        return usuario;
    }

    public Result VincularMinisterio(Guid ministerioId)
    {
        var existeVinculo = _ministerios.SingleOrDefault(x => x.MinisterioId == ministerioId);
        if (existeVinculo is null)
        {
            _ministerios.Add(new UsuarioMinisterio(ministerioId));
            return Result.Success();
        }

        return existeVinculo.Restore();
    }

    public Result DesvincularMinisterio(Guid ministerioId)
    {
        var usuarioMinisterio = _ministerios.SingleOrDefault(x => x.MinisterioId == ministerioId);
        if (usuarioMinisterio is null)
        {
            return UsuarioErrors.MinisterioNaoVinculadoAoUsuario;
        }

        return usuarioMinisterio.Remove();
    }

    public Result DefinirSenha(string senha)
    {
        if (senha.Length < 4)
        {
            return UsuarioErrors.SenhaCurta;
        }

        Senha = senha;

        return Result.Success();
    }
}

public enum EAuthType
{
    Local = 0,
    Google = 1,
}
