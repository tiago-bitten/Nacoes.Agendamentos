namespace Nacoes.Agendamentos.Infra.Extensions;

internal static class TaskExtensions
{
    public static async Task<T> OrElse<T>(this Task<T?> task, Func<Exception> exceptionFactory)
    {
        var result = await task.ConfigureAwait(false);
        if (result is null)
        {
            throw exceptionFactory();
        }

        return result;
    }
}
