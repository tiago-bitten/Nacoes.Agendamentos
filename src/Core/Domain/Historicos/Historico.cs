using Domain.Shared.Entities;
using Domain.Enums;

namespace Domain.Historicos;

public sealed class Historico : RemovableEntity
{
    private Historico() { }

    private Historico(
        Guid? entidadeId,
        DateTimeOffset data,
        Guid? usuarioId,
        string acao,
        EUserContextType usuarioAcao,
        string? detalhes)
    {
        EntidadeId = entidadeId;
        Data = data;
        UsuarioId = usuarioId;
        Acao = acao;
        UsuarioAcao = usuarioAcao;
        Detalhes = detalhes;
    }

    public Guid? EntidadeId { get; private set; }
    public DateTimeOffset Data { get; private set; }
    public Guid? UsuarioId { get; private set; }
    public string Acao { get; private set; } = string.Empty;
    public EUserContextType UsuarioAcao { get; private set; }
    public string? Detalhes { get; private set; }

    public static Historico Criar(
        Guid? entidadeId,
        Guid? usuarioId,
        string acao,
        EUserContextType usuarioAcao,
        string? detalhes)
    {
        if (string.IsNullOrEmpty(acao))
        {
            throw new ArgumentNullException(nameof(acao));
        }

        return new Historico(entidadeId, DateTimeOffset.UtcNow, usuarioId, acao, usuarioAcao, detalhes);
    }
}
