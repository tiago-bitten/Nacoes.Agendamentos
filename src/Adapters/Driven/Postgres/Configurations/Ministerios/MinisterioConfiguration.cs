using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Ministerios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Ministerios;

internal class MinisterioConfiguration : EntityIdConfiguration<Ministerio>
{
    public override void Configure(EntityTypeBuilder<Ministerio> builder)
    {
        base.Configure(builder);

        builder.Property(m => m.Nome);

        builder.Property(m => m.Descricao);

        builder.OwnsOne(m => m.Cor, corBuilder =>
        {
            corBuilder.Property(c => c.Valor)
                      .HasColumnName("cor");

            corBuilder.Property(c => c.Tipo)
                      .HasColumnName("cor_tipo")
                      .HasConversion<string>();
        });
    }
}
