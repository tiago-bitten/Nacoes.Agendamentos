using FluentValidation;

namespace Nacoes.Agendamentos.Application.Extensions;

public static class FluentValidationExtensions
{
    public static async Task CheckAsync<T>(this IValidator<T> validator, T instance)
    {
        var resultado = await validator.ValidateAsync(instance);

        if (!resultado.IsValid)
        {
            var erros = resultado.Errors.Select(x => x.ErrorMessage);
            throw new Exception(string.Join(", ", erros));
        }
    }
}
