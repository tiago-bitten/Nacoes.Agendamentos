using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public interface ILoginHandler
{
    Task<Result<LoginResponse>> ExecutarAsync(LoginCommand request, CancellationToken cancellationToken = default);
}
