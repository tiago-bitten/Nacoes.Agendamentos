using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Infra.Contexts;

public class NacoesDbContext : DbContext
{
    public NacoesDbContext(DbContextOptions<NacoesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<UsuarioAprovacao> UsuariosAprovacoes { get; set; }
    public DbSet<Agenda> Agendas { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    public DbSet<Voluntario> Voluntarios { get; set; }
    public DbSet<VoluntarioMinisterio> VoluntariosMinisterios { get; set; }
    public DbSet<Ministerio> Ministerios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NacoesDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
