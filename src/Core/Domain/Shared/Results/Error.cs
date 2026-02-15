namespace Domain.Shared.Results;

public record Error(string Code, string Description, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("General.Null", "The field cannot be null", ErrorType.Failure);

    public int StatusCode => Type switch
    {
        ErrorType.Failure => 500,
        ErrorType.Validation => 400,
        ErrorType.Problem => 400,
        ErrorType.NotFound => 404,
        ErrorType.Conflict => 409,
        _ => 418
    };

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Problem(string code, string description) =>
        new(code, description, ErrorType.Problem);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    public static implicit operator Result(Error error) => Result.Failure(error);
}
