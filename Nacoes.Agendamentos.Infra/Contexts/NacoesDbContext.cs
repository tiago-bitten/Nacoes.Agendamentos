using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Infra.Entities.DomainEvents;
using Nacoes.Agendamentos.Infra.Extensions;

namespace Nacoes.Agendamentos.Infra.Contexts;

internal class NacoesDbContext(DbContextOptions<NacoesDbContext> options,
                             IDomainEventsDispatcher domainEventsDispatcher) 
    : DbContext(options), INacoesDbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<UsuarioConvite> Convites { get; set; }
    public DbSet<Agenda> Agendas { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    public DbSet<Voluntario> Voluntarios { get; set; }
    public DbSet<VoluntarioMinisterio> VoluntariosMinisterios { get; set; }
    public DbSet<Ministerio> Ministerios { get; set; }
    public DbSet<Atividade> Atividades { get; set; }
    public DbSet<Historico> Historicos { get; set; }
    

    #region OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NacoesDbContext).Assembly);

        modelBuilder.ApplyValueObjectConverters();

        base.OnModelCreating(modelBuilder);
    }
    #endregion

    #region SaveChanges
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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
        return await base.SaveChangesAsync(cancellationToken);
    }

    private static void SaveAdded(EntityEntry entityEntry)
    {
        var newEntity = entityEntry.Entity;
        var type = newEntity.GetType();

        Console.WriteLine(type.Name + " criado.");
    }

    private static void SaveModified(EntityEntry entityEntry)
    {
        entityEntry.Property("DataCriacao").IsModified = false;
    }

    public List<IDomainEvent> GetDomainEvents()
    {
        var domainEvents = ChangeTracker.Entries<IEntity>()
                                        .Select(entry => entry.Entity)
                                        .SelectMany(entity =>
                                        {
                                            var domainEvents = entity.DomainEvents;
                                            entity.ClearDomainEvents();
                                            return domainEvents;
                                        }).ToList();
        return domainEvents;
    }

    public async Task PublishDomainEventsAsync(List<IDomainEvent> domainEvents)
    {
        await domainEventsDispatcher.DispatchAsync(domainEvents);
    }
    #endregion
}