using Microsoft.AspNetCore.Http.HttpResults;
using Nacoes.Agendamentos.Application.Common.Results;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public interface ILoginHandler
{
    Task<Result<LoginResponse, Error>> ExecutarAsync(LoginCommand request, CancellationToken cancellationToken = default);
}
