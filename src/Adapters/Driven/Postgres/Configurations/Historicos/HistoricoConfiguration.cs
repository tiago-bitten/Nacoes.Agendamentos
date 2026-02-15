using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Historicos;
using Postgres.Abstracts;

namespace Postgres.Configurations.Historicos;

internal sealed class HistoricoConfiguration : EntityIdConfiguration<AuditLog>
{
    public override void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        base.Configure(builder);

        builder.ToTable("historicos");

        builder.Property(h => h.EntityId)
               .HasColumnName("entidade_id")
               .IsRequired(false);

        builder.Property(h => h.Date)
               .HasColumnName("data")
               .IsRequired();

        builder.Property(h => h.UserId)
               .HasColumnName("usuario_id")
               .HasColumnType("uuid")
               .IsRequired(false);

        builder.Property(h => h.Action)
               .HasColumnName("acao")
               .HasMaxLength(AuditLog.ActionMaxLength)
               .IsRequired();

        builder.Property(h => h.UserAction)
               .HasColumnName("usuario_acao")
               .IsRequired();

        builder.Property(h => h.Details)
               .HasColumnName("detalhes")
               .HasMaxLength(AuditLog.DetailsMaxLength)
               .IsRequired(false);

        builder.Ignore(h => h.IsRemoved);
        builder.Ignore(h => h.CreatedAt);
    }
}
