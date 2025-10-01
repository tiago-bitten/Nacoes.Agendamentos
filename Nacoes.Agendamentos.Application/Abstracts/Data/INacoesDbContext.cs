using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nacoes.Agendamentos.Domain.Entities.Eventos;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;

namespace Nacoes.Agendamentos.Application.Abstracts.Data;

public interface INacoesDbContext
{
    DbSet<Usuario> Usuarios { get; set; }
    DbSet<UsuarioConvite> Convites { get; set; }
    DbSet<Evento> Eventos { get; set; }
    DbSet<Agendamento> Agendamentos { get; set; }
    DbSet<Voluntario> Voluntarios { get; set; }
    DbSet<VoluntarioMinisterio> VoluntariosMinisterios { get; set; }
    DbSet<Historico> Historicos { get; set; }
    DbSet<Ministerio> Ministerios { get; set; }
    DbSet<Atividade> Atividades { get; set; }
    DbSet<UsuarioConviteMinisterio> ConvitesMinisterios { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task PublishDomainEventsAsync(CancellationToken cancellationToken = default);
}