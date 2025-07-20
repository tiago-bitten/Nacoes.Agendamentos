using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Mappings;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;

public abstract class AdicionarUsuarioHandler(IUnitOfWork uow,
                                              IValidator<AdicionarUsuarioCommand> commandValidator,
                                              IUsuarioRepository usuarioRepository)
    : ICommandHandler<AdicionarUsuarioCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AdicionarUsuarioCommand command, CancellationToken cancellationToken = default)
    {
        var commandResult = await commandValidator.CheckAsync(command, cancellationToken);
        if (commandResult.IsFailure)
        {
            return commandResult.Error;
        }
        
        var existeUsuarioComEmail = await usuarioRepository.RecuperarPorEmailAddressAsync(command.Email);
        if (existeUsuarioComEmail is not null)
        {
            return UsuarioErrors.EmailEmUso;
        }
        
        var usuarioResult = command.ToDomain();
        if (usuarioResult.IsFailure)
        {
            return usuarioResult.Error;
        }
        
        await uow.BeginAsync();
        var usuario = usuarioResult.Value;
        await usuarioRepository.AddAsync(usuario);
        await uow.CommitAsync(cancellationToken);
        
        return Result<Guid>.Success(usuario.Id);
    }
}