using FluentValidation;
using FluentValidation.Results;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Abstracts.Behaviors;

internal static class ValidationDecorator
{
        internal sealed class CommandHandler<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> innerHandler,
                                                                  IEnumerable<IValidator<TCommand>> validators)
            : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
            {
                var validationFailures = await ValidateAsync(command, validators);
        
                if (validationFailures.Length == 0)
                {
                    return await innerHandler.Handle(command, cancellationToken);
                }
        
                return Result.Failure<TResponse>(CreateValidationError(validationFailures));
            }
        }

    internal sealed class CommandBaseHandler<TCommand>(ICommandHandler<TCommand> innerHandler,
                                                       IEnumerable<IValidator<TCommand>> validators) 
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var validationFailures = await ValidateAsync(command, validators);

            if (validationFailures.Length == 0)
            {
                return await innerHandler.Handle(command, cancellationToken);
            }

            return Result.Failure(CreateValidationError(validationFailures));
        }
    }

    private static async Task<ValidationFailure[]> ValidateAsync<TCommand>(TCommand command,
                                                                           IEnumerable<IValidator<TCommand>> validators)
    {
        var enumerable = validators as IValidator<TCommand>[] ?? validators.ToArray();
        if (enumerable.Length == 0)
        {
            return [];
        }

        var context = new ValidationContext<TCommand>(command);

        var validationResults = await Task.WhenAll(
            enumerable.Select(validator => validator.ValidateAsync(context)));

        var validationFailures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();

        return validationFailures;
    }

    private static ValidationError CreateValidationError(ValidationFailure[] validationFailures) =>
        new(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage)).ToArray());
}