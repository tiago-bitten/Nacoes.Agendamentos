namespace Application.Shared.Messaging;

public abstract record BaseQueryParam
{
    public int Limit { get; init; } = 10;
    public string? Cursor { get; init; }
}
