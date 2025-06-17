using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Authentication.Factories;
using Nacoes.Agendamentos.Application.Authentication.TokenGenerators;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarUsuario;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Infra.Extensions;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public sealed class LoginHandler(IUnitOfWork uow,
                                 IValidator<LoginCommand> loginValidtor,
                                 IAuthStrategyFactory authStrategyFactory,
                                 IAdicionarUsuarioHandler adicionarUsuarioHandler,
                                 ITokenGenerator tokenGenerator)
    : BaseHandler(uow), ILoginHandler
{
    public async Task<Result<LoginResponse, Error>> ExecutarAsync(LoginCommand command, CancellationToken cancellationToken)
    { 
        await loginValidtor.CheckAsync(command);

        var strategy = authStrategyFactory.Criar(command.AuthType);
        var usuario = await strategy.AutenticarAsync(command);

        if (usuario.NotPersisted())
        {
            var adicionarUsuarioCommand = new AdicionarUsuarioCommand
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                AuthType = usuario.AuthType,
            };

            await adicionarUsuarioHandler.ExecutarAsync(adicionarUsuarioCommand, cancellationToken);
        }

        /*var contaEstaAprovada = await GetSpecification(new UsuarioContaEstaAprovadaSpecfication());
        if (!contaEstaAprovada)
        {
            return UsuarioErrors.UsuarioContaNaoEstaAprovada;
        }*/

        var authToken = tokenGenerator.GenerateAuth(usuario);
        var refreshToken = tokenGenerator.GenerateRefresh();

        var loginResponse = new LoginResponse
        {
            AuthToken = authToken,
            RefreshToken = refreshToken
        };

        return loginResponse;
    }
}
