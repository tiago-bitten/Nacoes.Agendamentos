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
    
            if (validationFailures.Length is 0)
            {
                return await innerHandler.Handle(command, cancellationToken);
            }
    
            return CreateValidationError(validationFailures);
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(ICommandHandler<TCommand> innerHandler,
                                                       IEnumerable<IValidator<TCommand>> validators) 
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var validationFailures = await ValidateAsync(command, validators);

            if (validationFailures.Length is 0)
            {
                return await innerHandler.Handle(command, cancellationToken);
            }

            return CreateValidationError(validationFailures);
        }
    }

    private static async Task<ValidationFailure[]> ValidateAsync<TCommand>(TCommand command,
                                                                           IEnumerable<IValidator<TCommand>> validators)
    {
        var enumerable = validators as IValidator<TCommand>[] ?? validators.ToArray();
        if (enumerable.Length is 0)
        {
            return [];
        }

        var context = new ValidationContext<TCommand>(command);

        var validationRusultsTasks = enumerable.Select(validator => validator.ValidateAsync(context));
        var validationResults = await Task.WhenAll(validationRusultsTasks);

        var validationFailures = validationResults.Where(validationResult => !validationResult.IsValid)
                                                  .SelectMany(validationResult => validationResult.Errors)
                                                  .ToArray();
        return validationFailures;
    }

    private static Error CreateValidationError(ValidationFailure[] validationFailures)
    {
        var isSingleError = validationFailures.Length is 1;
        var firstErrorMessage = isSingleError ? "É obrigatório informar" : "São obrigatórios";
        var validationMessages = validationFailures.Select(x => x.ErrorMessage)
                                                   .ToArray()
                                                   .ToSingleMessage();
        return new Error("Validation.General", $"{firstErrorMessage}: {validationMessages}", ErrorType.Validation);
    }
}