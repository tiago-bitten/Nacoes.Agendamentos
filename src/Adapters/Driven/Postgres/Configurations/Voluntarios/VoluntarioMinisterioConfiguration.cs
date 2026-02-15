using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Voluntarios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Voluntarios;

internal sealed class VoluntarioMinisterioConfiguration : EntityIdConfiguration<VolunteerMinistry>
{
    public override void Configure(EntityTypeBuilder<VolunteerMinistry> builder)
    {
        base.Configure(builder);

        builder.ToTable("voluntario_ministerios");

        builder.Property(v => v.MinistryId)
            .HasColumnName("ministerio_id");
    }
}
