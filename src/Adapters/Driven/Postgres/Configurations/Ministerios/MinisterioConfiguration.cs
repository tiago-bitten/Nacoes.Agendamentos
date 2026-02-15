using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Ministerios;
using Domain.Shared.ValueObjects;
using Postgres.Abstracts;

namespace Postgres.Configurations.Ministerios;

internal sealed class MinisterioConfiguration : EntityIdConfiguration<Ministry>
{
    public override void Configure(EntityTypeBuilder<Ministry> builder)
    {
        base.Configure(builder);

        builder.ToTable("ministerios");

        builder.Property(m => m.Name)
            .HasColumnName("nome")
            .HasMaxLength(Ministry.NameMaxLength);

        builder.Property(m => m.Description)
            .HasColumnName("descricao")
            .HasMaxLength(Ministry.DescriptionMaxLength);

        builder.OwnsOne(m => m.Color, corBuilder =>
        {
            corBuilder.Property(c => c.Value)
                      .HasColumnName("cor");

            corBuilder.Property(c => c.Type)
                      .HasColumnName("cor_tipo")
                      .HasConversion(
                          v => v == EColorType.Hex ? "Hex" : v == EColorType.Rgb ? "Rgb" : "Hsl",
                          v => v == "Hex" ? EColorType.Hex : v == "Rgb" ? EColorType.Rgb : EColorType.Hsl);
        });
    }
}
