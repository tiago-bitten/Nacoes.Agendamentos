using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Mappings;
public static class UsuarioMapper
{
    public static Result<Usuario> ToDomain(this AdicionarUsuarioCommand command) 
        => Usuario.Criar(command.Nome,
                         command.Email,
                         command.AuthType,
                         command.Celular is null ? null : new Celular(command.Celular.Ddd, command.Celular.Numero),
                         command.Senha);
}
