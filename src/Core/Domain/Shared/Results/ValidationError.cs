namespace Domain.Shared.Results;

public sealed record ValidationError(Error[] Errors)
    : Error("Validation.General", $"{string.Join(", ", Errors.Select(e => e.Description))}", ErrorType.Validation)
{
    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
}
