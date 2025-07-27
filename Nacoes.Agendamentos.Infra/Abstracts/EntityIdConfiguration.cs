using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Infra.Abstracts;

internal abstract class EntityIdConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityId
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .HasColumnName("id")
               .HasDefaultValueSql("uuid_generate_v1()")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.DataCriacao)
               .HasColumnName("data_criacao");

        builder.Property(e => e.Inativo)
               .HasColumnName("inativo");
        
        builder.HasIndex(e => new { e.DataCriacao, e.Id })
            .HasDatabaseName($"ix_{typeof(T).Name.ToLower()}_data_criacao_id")
            .IsDescending(true, true);
    }
}
