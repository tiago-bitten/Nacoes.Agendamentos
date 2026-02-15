using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Usuarios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Usuarios;

internal sealed class UserConfiguration : EntityIdConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("usuarios");

        builder.Property(u => u.Name)
            .HasColumnName("nome")
            .HasMaxLength(User.NameMaxLength);

        builder.OwnsOne(u => u.Email, emailBuilder =>
        {
            emailBuilder.Property(e => e.Address)
                        .HasColumnName("email");

            emailBuilder.Property(e => e.IsConfirmed)
                        .HasColumnName("email_confirmado");

            emailBuilder.Property(e => e.ConfirmationCode)
                        .HasColumnName("email_codigo_confirmacao");

            emailBuilder.Property(e => e.ConfirmationCodeExpiration)
                        .HasColumnName("email_data_expiracao_codigo_confirmacao");
        });

        builder.Property(u => u.Password)
            .HasColumnName("senha");

        builder.OwnsOne(u => u.PhoneNumber, celularBuilder =>
        {
            celularBuilder.Property(c => c.Number)
                .HasColumnName("numero");

            celularBuilder.Property(c => c.AreaCode)
                .HasColumnName("ddd");
        });

        builder.Property(u => u.AuthType)
               .HasColumnName("auth_type")
               .HasConversion(
                   v => v == EAuthType.Local ? "Local" : "Google",
                   v => v == "Local" ? EAuthType.Local : EAuthType.Google);
    }
}
