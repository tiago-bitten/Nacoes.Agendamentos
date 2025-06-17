using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Usuarios;

public sealed class UsuarioAprovacaoRepository : BaseRepository<UsuarioAprovacao>, IUsuarioAprovacaoRepository
{
    #region Construtores
    public UsuarioAprovacaoRepository(NacoesDbContext dbContext) : base(dbContext)
    {
    }
    #endregion
}