using Domain.Shared.ValueObjects;

namespace Application.Common.Dtos;

public sealed record EventRecurrenceDto(EEventRecurrenceType Type, int? Interval, DateOnly? EndDate);
