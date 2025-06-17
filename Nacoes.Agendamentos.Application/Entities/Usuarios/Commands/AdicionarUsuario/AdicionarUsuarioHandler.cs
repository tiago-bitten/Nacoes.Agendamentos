using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Authentication.Factories;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Mappings;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarUsuario;

public sealed class AdicionarUsuarioHandler(IUnitOfWork uow,
                                            IValidator<AdicionarUsuarioCommand> usuarioValidator,
                                            IUsuarioRepository usuarioRepository,
                                            IAuthStrategyFactory authStrategyFactory)
    : BaseHandler(uow), IAdicionarUsuarioHandler
{

    public async Task<Result<Id<Usuario>, Error>> ExecutarAsync(AdicionarUsuarioCommand command, CancellationToken cancellation = default)
    {
        await usuarioValidator.CheckAsync(command);

        var usuario = command.GetEntidade();

        /*var emailExistente = await GetSpecification(new UsuarioComEmailExistenteSpecification(usuario.Email),
                                                    usuarioRepository);
        if (emailExistente)
        {
            return UsuarioErrors.UsuarioComEmailExistente;
        }*/

        if (command.AuthType != EAuthType.Local)
        {
            var strategy = authStrategyFactory.Criar(usuario.AuthType);
            usuario = await strategy.AutenticarAsync(new LoginCommand
            {
                TokenExterno = command.TokenExterno
            });
        }

        await Uow.BeginAsync();
        
        await usuarioRepository.AddAsync(usuario);
        usuario.SolicitarAprovacao(command.MinisteriosIds);

        await Uow.CommitAsync(cancellation);

        return usuario.Id;
    }
}