using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;

namespace Nacoes.Agendamentos.Application.Abstracts.Data;

public interface INacoesDbContext
{
    DbSet<Usuario> Usuarios { get; set; }
    DbSet<UsuarioConvite> Convites { get; set; }
    DbSet<Agenda> Agendas { get; set; }
    DbSet<Agendamento> Agendamentos { get; set; }
    DbSet<Voluntario> Voluntarios { get; set; }
    DbSet<VoluntarioMinisterio> VoluntariosMinisterios { get; set; }
    DbSet<Historico> Historicos { get; set; }
    DbSet<Ministerio> Ministerios { get; set; }
    DbSet<Atividade> Atividades { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}