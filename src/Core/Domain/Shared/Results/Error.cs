namespace Domain.Shared.Results;

public record Error(string Codigo, string Descricao, ErrorType Tipo)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("Geral.Null", "O campo não pode ser nulo", ErrorType.Failure);

    public int StatusCode => Tipo switch
    {
        ErrorType.Failure => 500,
        ErrorType.Validation => 400,
        ErrorType.Problem => 400,
        ErrorType.NotFound => 404,
        ErrorType.Conflict => 409,
        _ => 418
    };

    public static Error Failure(string codigo, string descricao) =>
        new(codigo, descricao, ErrorType.Failure);

    public static Error NotFound(string codigo, string descricao) =>
        new(codigo, descricao, ErrorType.NotFound);

    public static Error Problem(string codigo, string descricao) =>
        new(codigo, descricao, ErrorType.Problem);

    public static Error Conflict(string codigo, string descricao) =>
        new(codigo, descricao, ErrorType.Conflict);

    public static implicit operator Result(Error error) => Result.Failure(error);
}
