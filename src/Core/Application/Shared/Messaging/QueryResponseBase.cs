namespace Application.Shared.Messaging;

public abstract record QueryResponseBase
{
    public string? NextCursor { get; init; }
    public required int Total { get; init; }
}
