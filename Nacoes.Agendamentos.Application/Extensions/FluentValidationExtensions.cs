using FluentValidation;
using FluentValidation.Results;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Extensions;

public static class FluentValidationExtensions
{
    public static async Task<Result> CheckAsync<T>(this IValidator<T> validator, T instance, CancellationToken cancellation = default)
    {
        var resultado = await validator.ValidateAsync(instance, cancellation);
        if (!resultado.IsValid)
        {
            return resultado.ToError();
        }
        
        return Result.Success();
    }
    
    public static Error ToError(this ValidationResult validationResult)
    {
        var erros = validationResult.Errors.Select(x => x.ErrorMessage);
        var errosStr = string.Join(", ", erros);
        return new Error("Validacao", errosStr);
    }
}
