using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Usuarios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Usuarios;

internal sealed class UsuarioConviteMinisterioConfiguration : EntityIdConfiguration<UsuarioConviteMinisterio>
{
    public override void Configure(EntityTypeBuilder<UsuarioConviteMinisterio> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.ConviteId)
            .IsRequired();

        builder.Property(u => u.MinisterioId)
            .IsRequired();

        builder.HasOne(u => u.Convite)
            .WithMany(u => u.Ministerios)
            .HasForeignKey(u => u.ConviteId);

        builder.HasOne(u => u.Ministerio)
            .WithMany()
            .HasForeignKey(u => u.MinisterioId);
    }
}
