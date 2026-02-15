using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Usuarios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Usuarios;

internal sealed class UsuarioConviteConfiguration : EntityIdConfiguration<UserInvitation>
{
    public override void Configure(EntityTypeBuilder<UserInvitation> builder)
    {
        base.Configure(builder);

        builder.ToTable("usuario_convites");

        builder.Property(u => u.Name)
               .HasColumnName("nome")
               .HasMaxLength(UserInvitation.NameMaxLength)
               .IsRequired();

        builder.OwnsOne(u => u.Email, emailBuilder =>
        {
            emailBuilder.Property(e => e.Address)
                        .HasColumnName("email")
                        .IsRequired();

            emailBuilder.Ignore(e => e.ConfirmationCode);
            emailBuilder.Ignore(e => e.ConfirmationCodeExpiration);
            emailBuilder.Ignore(e => e.IsConfirmed);
        });

        builder.Property(u => u.SentById)
               .HasColumnName("enviado_por_id")
               .IsRequired();

        builder.Property(u => u.SentToId)
               .HasColumnName("enviado_para_id")
               .IsRequired(false);

        builder.Property(u => u.Status)
               .HasColumnName("status")
               .HasConversion(
                   v => v == EInvitationStatus.Pending ? "Pendente"
                       : v == EInvitationStatus.Accepted ? "Aceito"
                       : v == EInvitationStatus.Declined ? "Recusado"
                       : v == EInvitationStatus.Expired ? "Expirado"
                       : v == EInvitationStatus.Cancelled ? "Cancelado"
                       : "Erro",
                   v => v == "Pendente" ? EInvitationStatus.Pending
                       : v == "Aceito" ? EInvitationStatus.Accepted
                       : v == "Recusado" ? EInvitationStatus.Declined
                       : v == "Expirado" ? EInvitationStatus.Expired
                       : v == "Cancelado" ? EInvitationStatus.Cancelled
                       : EInvitationStatus.Error)
               .IsRequired();

        builder.Property(u => u.Reason)
               .HasColumnName("motivo")
               .HasMaxLength(UserInvitation.ReasonMaxLength)
               .IsRequired(false);

        builder.Property(u => u.ExpirationDate)
               .HasColumnName("data_expiracao")
               .IsRequired();

        builder.Property(u => u.Token)
               .HasColumnName("token")
               .HasMaxLength(UserInvitation.TokenMaxLength)
               .IsRequired();

        builder.HasOne(u => u.SentBy)
               .WithMany()
               .HasForeignKey(u => u.SentById);

        builder.HasOne(u => u.SentTo)
               .WithMany()
               .HasForeignKey(u => u.SentToId);
    }
}
