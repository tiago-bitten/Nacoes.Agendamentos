﻿using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nacoes.Agendamentos.Application.Common.Results;

public class Result<TValue, TError>
{
    public readonly TValue? Value;
    public readonly TError? Error;

    private bool _isSuccess;

    private Result(TValue value)
    {
        _isSuccess = true;
        Value = value;
        Error = default;
    }

    private Result(TError error)
    {
        _isSuccess = false;
        Value = default;
        Error = error;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);

    public static implicit operator Result<TValue, TError>(TError error) => new(error);

    public TResult Match<TResult>(Func<TValue, TResult> success, Func<TError, TResult> failure)
    {
        return _isSuccess ? success(Value!) : failure(Error!);
    }
}