using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Infra.Extensions;

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

    #region OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NacoesDbContext).Assembly);

        modelBuilder.ApplyValueObjectConverters();

        base.OnModelCreating(modelBuilder);
    }
    #endregion

    #region SaveChanges
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entityEntries = ChangeTracker.Entries()
                                         .Where(x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entityEntries)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    SaveAdded(entity);
                    break;

                case EntityState.Modified:
                    SaveModified(entity);
                    break;
            }
        }


        return base.SaveChangesAsync(cancellationToken);
    }

    private void SaveAdded(EntityEntry entityEntry)
    {
        var newEntity = entityEntry.Entity;
        var type = newEntity.GetType();

        if (type.IsEntityId())
        {
            ((dynamic)newEntity).Salvar(); 
        }
    }

    private void SaveModified(EntityEntry entityEntry)
    {
        entityEntry.Property("DataCriacao").IsModified = false;
    }
    #endregion
}