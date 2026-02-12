using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Voluntarios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Voluntarios;

internal class VoluntarioMinisterioConfiguration : EntityIdConfiguration<VoluntarioMinisterio>
{
    public override void Configure(EntityTypeBuilder<VoluntarioMinisterio> builder)
    {
        base.Configure(builder);

        builder.Property(v => v.MinisterioId);
    }
}
