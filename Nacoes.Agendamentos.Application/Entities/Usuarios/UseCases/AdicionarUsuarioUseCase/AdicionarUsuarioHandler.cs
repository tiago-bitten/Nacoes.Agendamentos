using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;

public sealed class AdicionarUsuarioHandler(IUnitOfWork uow,
                                            IValidator<AdicionarUsuarioCommand> usuarioValidator,
                                            IUsuarioRepository usuarioRepository)
    : BaseHandler(uow), IAdicionarUsuarioHandler
{

    public async Task<Id<Usuario>> ExecutarAsync(AdicionarUsuarioCommand command)
    {
        await usuarioValidator.CheckAsync(command);

        throw new NotImplementedException();
    }
}
