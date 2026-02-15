using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Usuarios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Usuarios;

internal sealed class UsuarioConviteMinisterioConfiguration : EntityIdConfiguration<UserInvitationMinistry>
{
    public override void Configure(EntityTypeBuilder<UserInvitationMinistry> builder)
    {
        base.Configure(builder);

        builder.ToTable("usuario_convite_ministerios");

        builder.Property(u => u.InvitationId)
            .HasColumnName("convite_id")
            .IsRequired();

        builder.Property(u => u.MinistryId)
            .HasColumnName("ministerio_id")
            .IsRequired();

        builder.HasOne(u => u.Invitation)
            .WithMany(u => u.Ministries)
            .HasForeignKey(u => u.InvitationId);

        builder.HasOne(u => u.Ministry)
            .WithMany()
            .HasForeignKey(u => u.MinistryId);
    }
}
