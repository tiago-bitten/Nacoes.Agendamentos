namespace Nacoes.Agendamentos.Domain.Common;

public sealed record Error(string Code, ErrorType Type, string? Description = null)
{
    public static readonly Error None = new(string.Empty, 0, string.Empty);
    
    public int GetStatusCode() => Type switch
    {
        ErrorType.Validation => 400,
        ErrorType.NotFound => 404,
        ErrorType.Internal => 500,
        ErrorType.Unauthorized => 401,
        _ => 418 // im a teapot
    };

    public static implicit operator Result(Error error) => Result.Failure(error);
}

public enum ErrorType
{
    Validation = 0,
    NotFound = 1,
    Internal = 2,
    Unauthorized = 3
}