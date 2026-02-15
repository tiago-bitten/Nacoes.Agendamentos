namespace Application.Shared.Messaging;

public abstract record PagedQuery<TResponse>(
    string? Cursor = null,
    int PageSize = 20) : IQuery<TResponse>;
