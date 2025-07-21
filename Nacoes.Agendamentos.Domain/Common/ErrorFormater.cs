﻿namespace Nacoes.Agendamentos.Domain.Common;

public static class ErrorFormatter
{
    public static string FormatList(List<string>? items)
    {
        if (items is null || items.Count is 0)
        {
            return string.Empty;
        }

        if (items.Count is 1)
        {
            return items[0];
        }

        var lastItem = items.Last();
        var otherItems = items.Take(items.Count - 1);

        return $"{string.Join(", ", otherItems)} e {lastItem}";
    }
}