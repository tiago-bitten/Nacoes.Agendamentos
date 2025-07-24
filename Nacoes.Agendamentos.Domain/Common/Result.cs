using System.Diagnostics.CodeAnalysis;

namespace Nacoes.Agendamentos.Domain.Common;

public class Result
{
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public int StatusCode => IsSuccess ? 200 : Error.StatusCode;
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new(false, default!, error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    public Result(bool isSuccess, T? value, Error error)
        : base(isSuccess, error) => _value = value;

    [NotNull]
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Não é possível obter o valor de um Result falho.");

    public static Result<T> Success(T value) => new(true, value, Error.None);
    private new static Result<T> Failure(Error error) => new(false, default!, error);

    public static implicit operator Result<T>(Error error) => Failure(error);
    
    public static Result<T> ValidationFailure(Error error) => new(false, default, error);
}
