using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Voluntarios;
using Domain.Shared.ValueObjects;
using Postgres.Abstracts;

namespace Postgres.Configurations.Voluntarios;

internal class VoluntarioConfiguration : EntityIdConfiguration<Voluntario>
{
    public override void Configure(EntityTypeBuilder<Voluntario> builder)
    {
        base.Configure(builder);

        builder.Property(v => v.Nome);

        builder.OwnsOne(v => v.Email, emailBuilder =>
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

        builder.OwnsOne(v => v.Celular, celularBuilder =>
        {
            celularBuilder.Property(c => c.Numero);

            celularBuilder.Property(c => c.Ddd);
        });

        builder.OwnsOne(v => v.Cpf, cpfBuilder =>
        {
            cpfBuilder.Property(c => c.Numero)
                      .HasColumnName("cpf");
        });

        builder.OwnsOne(v => v.DataNascimento, dataNascimentoBuilder =>
        {
            dataNascimentoBuilder.Property(d => d.Valor)
                                 .HasColumnName("data_nascimento");
        });

        builder.HasMany(v => v.Ministerios)
               .WithOne()
               .HasForeignKey("voluntario_id");
    }
}
