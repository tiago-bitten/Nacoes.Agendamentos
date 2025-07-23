using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Usuarios.Configurations;

internal class UsuarioConfiguration : EntityIdConfiguration<Usuario>
{
    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.Nome);

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

        builder.Property(u => u.Senha);

        builder.OwnsOne(u => u.Celular, celularBuilder =>
        {
            celularBuilder.Property(c => c.Numero);

            celularBuilder.Property(c => c.Ddd);
        });

        builder.Property(u => u.AuthType)
               .HasConversion<string>();
    }
}
