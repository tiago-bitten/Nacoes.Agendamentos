using Domain.Shared.ValueObjects;

namespace Application.Common.Dtos;

public sealed record ColorDto(string Value, EColorType Type);
