using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Historicos;

// TODO: Futuramente isso será em nosql, não em sql
// A parte de geração de Id será diferente, por hora pode ficar assim
public sealed class Historico : EntityId
{
    private Historico() { }
    
    private Historico(Guid? entidadeId, 
                      DateTimeOffset data,
                      Guid? usuarioId,
                      string acao,
                      EHistoricoUsuarioAcao usuarioAcao,
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
    public EHistoricoUsuarioAcao UsuarioAcao { get; private set; }
    public string? Detalhes { get; private set; }

    public static Historico Criar(
        Guid? entidadeId,
        DateTimeOffset data,
        Guid? usuarioId,
        string acao,
        EHistoricoUsuarioAcao usuarioAcao,
        string? detalhes)
    {
        if (string.IsNullOrEmpty(acao))
        {
            throw new ArgumentNullException(nameof(acao));
        }
        
        if (usuarioAcao == EHistoricoUsuarioAcao.NotDefined)
        {
            throw new ArgumentNullException(nameof(usuarioAcao));
        }
        
        return new Historico(entidadeId, data, usuarioId, acao, usuarioAcao, detalhes);
    }
}

public enum EHistoricoUsuarioAcao
{
    NotDefined = 0,
    Usuario = 1,
    ThirdPartyUser = 2,
    Bot = 3
}