using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Ministerios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Ministerios;

internal sealed class AtividadeConfiguration : EntityIdConfiguration<Activity>
{
    public override void Configure(EntityTypeBuilder<Activity> builder)
    {
        base.Configure(builder);

        builder.ToTable("atividades");

        builder.Property(a => a.Name)
            .HasColumnName("nome")
            .HasMaxLength(Activity.NameMaxLength);

        builder.Property(a => a.Description)
            .HasColumnName("descricao")
            .HasMaxLength(Activity.DescriptionMaxLength);

        builder.Property(a => a.MinistryId)
            .HasColumnName("ministerio_id");

        builder.HasOne(a => a.Ministry)
            .WithMany(m => m.Activities)
            .HasForeignKey(a => a.MinistryId);
    }
}
