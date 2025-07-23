using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Infra.DomainEvents;
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


        var result = await base.SaveChangesAsync(cancellationToken);
        
        await PublishDomainEventsAsync();
        
        return result;
    }

    private static void SaveAdded(EntityEntry entityEntry)
    {
        var newEntity = entityEntry.Entity;
        var type = newEntity.GetType();

        if (type.IsEntityId())
        {
            // TODO: incrementar o id de um jeito mais inteligente
            ((dynamic)newEntity).Salvar(); 
        }
    }

    private static void SaveModified(EntityEntry entityEntry)
    {
        entityEntry.Property("DataCriacao").IsModified = false;
    }
    
    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker.Entries<IEntity>()
                                        .Select(entry => entry.Entity)
                                        .SelectMany(entity => 
                                        { 
                                            var domainEvents = entity.DomainEvents;
                                            entity.ClearDomainEvents();
                                            return domainEvents; 
                                        }).ToList();

        await domainEventsDispatcher.DispatchAsync(domainEvents);
    }
    #endregion
}