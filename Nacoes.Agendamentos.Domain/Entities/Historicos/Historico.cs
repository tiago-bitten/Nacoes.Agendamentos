using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Historicos;

// TODO: Futuramente isso será em nosql, não em sql
// A parte de geração de Id será diferente, por hora pode ficar assim
public sealed class Historico : EntityId
{
    private Historico() { }
    
    private Historico(Guid? entidadeId, DateTimeOffset data, Guid? usuarioId, string acao, EHistoricoTipoAcao tipoAcao, string? detalhes)
    {
        EntidadeId = entidadeId;
        Data = data;
        UsuarioId = usuarioId;
        Acao = acao;
        TipoAcao = tipoAcao;
        Detalhes = detalhes;
    }
    
    public Guid? EntidadeId { get; private set; }
    public DateTimeOffset Data { get; private set; }
    public Guid? UsuarioId { get; private set; }
    public string Acao { get; private set; } = string.Empty;
    public EHistoricoTipoAcao TipoAcao { get; private set; }
    public string? Detalhes { get; private set; }
    
    public static Historico Criar(Guid? entidadeId, DateTimeOffset data, Guid? usuarioId, string acao, 
        EHistoricoTipoAcao tipoAcao, string? detalhes) => new(entidadeId, data, usuarioId, acao, tipoAcao, detalhes);
}

public enum EHistoricoTipoAcao
{
    Criar = 0,
    Atualizar = 1,
    Remover = 2,
    Login = 3,
    Outro = 4
}