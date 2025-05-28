using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Infra.Abstracts;

public abstract class EntityIdConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityId<T>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .HasColumnName("id");

        builder.Property(e => e.DataCriacao)
               .HasColumnName("created_at");

        builder.Property(e => e.Inativo)
               .HasColumnName("inactive");

        builder.HasIndex(e => new { e.Id, e.DataCriacao });
    }
}
