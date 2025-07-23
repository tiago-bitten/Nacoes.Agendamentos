namespace Nacoes.Agendamentos.Domain.Common;

public record Error(string Codigo, string Descricao, ErrorType Tipo)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("Geral.Null", "O campo não pode ser nulo", ErrorType.Failure);

    public int GetStatusCode => Tipo switch
    {
        ErrorType.Failure => 500,
        ErrorType.Validation => 400,
        ErrorType.Problem => 500,
        ErrorType.NotFound => 404,
        ErrorType.Conflict => 409,
        _ => 418 // teapot
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

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    Problem = 2,
    NotFound = 3,
    Conflict = 4
}