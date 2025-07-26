using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.ValueObjects;
using EnviadoPorId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Usuarios.Usuario>;
using EnviadoParaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Usuarios.Usuario>;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class UsuarioConvite : EntityId<UsuarioConvite>, IAggregateRoot
{
    private const int ExpiracaoEmDias = 7;
    
    private UsuarioConvite() { }
    
    private UsuarioConvite(string nome, Email email, 
                           EnviadoPorId enviadopor,
                           EConviteStatus status,
                           DateTimeOffset dataExpiracao,
                           string token)
    {
        Nome = nome;
        Email = email;
        EnviadoPorId = enviadopor;
        Status = status;
        DataExpiracao = dataExpiracao;
        Token = token;
    }
    
    public string Nome { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public EnviadoPorId EnviadoPorId { get; private set; } = null!;
    public EnviadoParaId? EnviadoParaId { get; private set; }
    public EConviteStatus Status { get; private set; }
    public string Motivo { get; private set; } = null!;
    public DateTimeOffset DataExpiracao { get; private set; }
    public string Token { get; private set; } = null!;

    public Usuario EnviadoPor { get; private set; } = null!;
    public Usuario? EnviadoPara { get; private set; }
    
    public string Path => $"usuarios/convites/{Token}";
    
    #region Criar
    public static Result<UsuarioConvite> Criar(string nome, Email email, EnviadoPorId enviadoPorId)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return UsuarioErrors.NomeObrigatorio;
        }
        
        var dataExpiracao = DateTimeOffset.UtcNow.AddDays(ExpiracaoEmDias);
        var token = Guid.NewGuid().ToString("N");
        var usuarioConvite = new UsuarioConvite(nome, email, enviadoPorId, EConviteStatus.Pendente, dataExpiracao, token);
        
        return Result<UsuarioConvite>.Success(usuarioConvite);
    }
    #endregion
    
    #region Aceitar
    public Result Aceitar(EnviadoParaId enviadoParaId)
    {
        if (Status is not EConviteStatus.Pendente)
        {
            return UsuarioConviteErrors.StatusInvalidoParaAceitar;
        }

        Status = EConviteStatus.Aceito;
        EnviadoParaId = enviadoParaId;

        return Result.Success();
    }
    #endregion
    
    #region Recusar
    public Result Recusar()
    {
        if (Status is not EConviteStatus.Pendente)
        {
            return UsuarioConviteErrors.StatusInvalidoParaRecusar;
        }

        Status = EConviteStatus.Recusado;

        return Result.Success();
    }
    #endregion
    
    #region Expirar
    public Result Expirar()
    {
        if (Status is not EConviteStatus.Pendente)
        {
            return UsuarioConviteErrors.StatusInvalidoParaRecusar;
        }
        
        var dataHoje = DateTimeOffset.UtcNow;
        if (DataExpiracao > dataHoje)
        {
            return UsuarioConviteErrors.DataExpiracaoNaoAtingida;
        }
        
        Status = EConviteStatus.Expirado;

        return Result.Success();
    }
    #endregion
    
    #region Cancelar
    public Result Cancelar(string motivo)
    {
        if (Status is not EConviteStatus.Pendente)
        {
            return UsuarioConviteErrors.StatusInvalidoParaCancelar;
        }
        
        if (string.IsNullOrWhiteSpace(motivo))
        {
            return UsuarioConviteErrors.MotivoObrigatorio;
        }

        Status = EConviteStatus.Cancelado;
        Motivo = motivo;

        return Result.Success();
    }
    #endregion
    
    #region Erro
    public Result Erro(string? motivo)
    {
        Status = EConviteStatus.Erro;
        Motivo = motivo ?? "Ocorreu um erro interno.";

        return Result.Success();
    }
    #endregion
}

public enum EConviteStatus
{
    Pendente = 0,
    Aceito = 1,
    Recusado = 2,
    Expirado = 3,
    Cancelado = 4,
    Erro = 5
}