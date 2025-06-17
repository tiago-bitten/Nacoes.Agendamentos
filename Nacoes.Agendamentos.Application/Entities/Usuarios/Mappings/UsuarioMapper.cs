using Nacoes.Agendamentos.Application.Authentication.PasswordVerifiers;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarUsuario;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Mappings;
public static class UsuarioMapper
{
    public static Usuario GetEntidade(this AdicionarUsuarioCommand command)
        => new(command.Nome, 
               new Email(command.Email),
               command.AuthType,
               command.Celular != null ? new Celular(command.Celular.Ddd, command.Celular.Numero) : null,
               string.IsNullOrEmpty(command.Senha) ? null : PasswordVerifier.HashPassword(command.Senha));
}
