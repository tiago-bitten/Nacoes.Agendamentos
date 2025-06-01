using Nacoes.Agendamentos.Domain.Exceptions;

namespace Nacoes.Agendamentos.Infra.Extensions;

public static class TaskExtensions
{
    public static async Task<T> OrElse<T>(this Task<T?> task, Func<DomainException> exceptionFactory)
    {
        var result = await task.ConfigureAwait(false);
        if (result is null)
        {
            throw exceptionFactory();
        }

        return result;
    }
}
