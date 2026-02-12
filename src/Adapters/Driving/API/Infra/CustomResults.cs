using Domain.Shared.Results;

namespace API.Infra;

public static class CustomResults
{
    public static IResult NoContent() => Results.Ok();

    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.Tipo),
            statusCode: result.Error.StatusCode,
            extensions: GetErrors(result));

        static string GetTitle(Error error) =>
            error.Tipo switch
            {
                ErrorType.Validation => error.Codigo,
                ErrorType.Problem => error.Codigo,
                ErrorType.NotFound => error.Codigo,
                ErrorType.Conflict => error.Codigo,
                _ => "Server failure"
            };

        static string GetDetail(Error error) =>
            error.Tipo switch
            {
                ErrorType.Validation => error.Descricao,
                ErrorType.Problem => error.Descricao,
                ErrorType.NotFound => error.Descricao,
                ErrorType.Conflict => error.Descricao,
                _ => "An unexpected error occurred"
            };

        static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

        static Dictionary<string, object?>? GetErrors(Result result)
        {
            if (result.Error is not ValidationError validationError)
            {
                return null;
            }

            return new Dictionary<string, object?>
            {
                { "errors", validationError.Errors }
            };
        }
    }
}
