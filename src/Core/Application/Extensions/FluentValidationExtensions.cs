using FluentValidation;
using FluentValidation.Results;
using Domain.Shared.Results;

namespace Application.Extensions;

public static class FluentValidationExtensions
{
    public static async Task<Result> CheckAsync<T>(this IValidator<T> validator, T instance, CancellationToken ct = default)
    {
        var result = await validator.ValidateAsync(instance, ct);
        if (!result.IsValid)
        {
            return result.ToError();
        }

        return Result.Success();
    }

    public static Error ToError(this ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select(x => x.ErrorMessage);
        var errorsStr = string.Join(", ", errors);
        return Error.Failure("Validation", errorsStr);
    }
}
