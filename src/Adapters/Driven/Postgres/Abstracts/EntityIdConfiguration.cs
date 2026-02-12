using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Shared.Entities;

namespace Postgres.Abstracts;

internal abstract class EntityIdConfiguration<T> : IEntityTypeConfiguration<T> where T : RemovableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(e => e.CreatedAt)
            .HasColumnName("data_criacao")
            .IsRequired();

        builder.Property(e => e.IsRemoved)
            .HasColumnName("inativo")
            .IsRequired();

        builder.HasIndex(e => new { e.CreatedAt, e.Id })
            .HasDatabaseName($"ix_{typeof(T).Name.ToLower()}_data_criacao_id")
            .IsDescending(true, true);
    }
}
