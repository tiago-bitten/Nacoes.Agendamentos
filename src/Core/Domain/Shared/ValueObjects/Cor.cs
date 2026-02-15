namespace Domain.Shared.ValueObjects;

public enum EColorType
{
    Hex = 0,
    Rgb = 1,
    Hsl = 2
}

public sealed record class Color
{
    public string Value { get; }
    public EColorType Type { get; }

    public Color(string value, EColorType type)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Color value cannot be empty.", nameof(value));
        }

        Value = value;
        Type = type;
    }

    public static Color Default => new("#1F77B4", EColorType.Hex);

    public string ToCssString()
    {
        return Type switch
        {
            EColorType.Hex => NormalizeHex(Value),
            EColorType.Rgb => NormalizeRgb(Value),
            EColorType.Hsl => NormalizeHsl(Value),
            _ => Value
        };
    }

    private static string NormalizeHex(string hex)
    {
        if (!hex.StartsWith($"#"))
        {
            hex = "#" + hex;
        }

        if (hex.Length != 7)
        {
            throw new ArgumentException("Invalid Hex format.");
        }

        return hex.ToUpperInvariant();
    }

    private static string NormalizeRgb(string rgb)
    {
        var values = rgb.Replace("rgb(", "").Replace(")", "").Split(',');
        if (values.Length != 3)
        {
            throw new ArgumentException("Invalid RGB format.");
        }

        int r = int.Parse(values[0].Trim());
        int g = int.Parse(values[1].Trim());
        int b = int.Parse(values[2].Trim());
        return $"rgb({r}, {g}, {b})";
    }

    private static string NormalizeHsl(string hsl)
    {
        var values = hsl.Replace("hsl(", "").Replace(")", "").Replace("%", "").Split(',');
        if (values.Length != 3)
        {
            throw new ArgumentException("Invalid HSL format.");
        }

        int h = int.Parse(values[0].Trim());
        int s = int.Parse(values[1].Trim());
        int l = int.Parse(values[2].Trim());
        return $"hsl({h}, {s}%, {l}%)";
    }

    public Color ConvertTo(EColorType newType)
    {
        if (newType == Type)
        {
            return this;
        }

        return newType switch
        {
            EColorType.Hex => ConvertToHex(),
            EColorType.Rgb => ConvertToRgb(),
            EColorType.Hsl => ConvertToHsl(),
            _ => throw new NotSupportedException($"Conversion to type {newType} is not supported.")
        };
    }

    private Color ConvertToHex()
    {
        return Type switch
        {
            EColorType.Rgb => new Color(RgbToHex(Value), EColorType.Hex),
            EColorType.Hsl => new Color(RgbToHex(HslToRgb(Value)), EColorType.Hex),
            _ => throw new NotSupportedException("Conversion to Hex is not supported for this type.")
        };
    }

    private Color ConvertToRgb()
    {
        return Type switch
        {
            EColorType.Hex => new Color(HexToRgb(Value), EColorType.Rgb),
            EColorType.Hsl => new Color(HslToRgb(Value), EColorType.Rgb),
            _ => throw new NotSupportedException("Conversion to Rgb is not supported for this type.")
        };
    }

    private Color ConvertToHsl()
    {
        return Type switch
        {
            EColorType.Rgb => new Color(RgbToHsl(Value), EColorType.Hsl),
            EColorType.Hex => new Color(RgbToHsl(HexToRgb(Value)), EColorType.Hsl),
            _ => throw new NotSupportedException("Conversion to Hsl is not supported for this type.")
        };
    }

    private static string RgbToHex(string rgb)
    {
        var values = rgb.Replace("rgb(", "").Replace(")", "").Split(',');
        if (values.Length != 3)
        {
            throw new ArgumentException("Invalid RGB format.");
        }

        int r = int.Parse(values[0].Trim());
        int g = int.Parse(values[1].Trim());
        int b = int.Parse(values[2].Trim());

        return $"#{r:X2}{g:X2}{b:X2}";
    }

    private static string HexToRgb(string hex)
    {
        if (hex.StartsWith("#"))
        {
            hex = hex[1..];
        }

        if (hex.Length != 6)
        {
            throw new ArgumentException("Invalid Hex format.");
        }

        var r = Convert.ToInt32(hex.Substring(0, 2), 16);
        var g = Convert.ToInt32(hex.Substring(2, 2), 16);
        var b = Convert.ToInt32(hex.Substring(4, 2), 16);

        return $"rgb({r}, {g}, {b})";
    }

    private static string RgbToHsl(string rgb)
    {
        var values = rgb.Replace("rgb(", "").Replace(")", "").Split(',');
        if (values.Length != 3)
        {
            throw new ArgumentException("Invalid RGB format.");
        }

        double r = int.Parse(values[0].Trim()) / 255.0;
        double g = int.Parse(values[1].Trim()) / 255.0;
        double b = int.Parse(values[2].Trim()) / 255.0;

        double max = Math.Max(r, Math.Max(g, b));
        double min = Math.Min(r, Math.Min(g, b));
        double h = 0, s = 0, l = (max + min) / 2.0;

        if (max != min)
        {
            double d = max - min;
            s = l > 0.5 ? d / (2.0 - max - min) : d / (max + min);

            if (max == r)
            {
                h = (g - b) / d + (g < b ? 6 : 0);
            }
            else if (max == g)
            {
                h = (b - r) / d + 2;
            }
            else
            {
                h = (r - g) / d + 4;
            }

            h /= 6;
        }

        int hValue = (int)(h * 360);
        int sValue = (int)(s * 100);
        int lValue = (int)(l * 100);

        return $"hsl({hValue}, {sValue}%, {lValue}%)";
    }

    private static string HslToRgb(string hsl)
    {
        var values = hsl.Replace("hsl(", "").Replace(")", "").Replace("%", "").Split(',');
        if (values.Length != 3)
        {
            throw new ArgumentException("Invalid HSL format.");
        }

        double h = double.Parse(values[0].Trim()) / 360.0;
        double s = double.Parse(values[1].Trim()) / 100.0;
        double l = double.Parse(values[2].Trim()) / 100.0;

        double r, g, b;

        if (s == 0)
        {
            r = g = b = l;
        }
        else
        {
            double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
            double p = 2 * l - q;
            r = HueToRgb(p, q, h + 1.0 / 3);
            g = HueToRgb(p, q, h);
            b = HueToRgb(p, q, h - 1.0 / 3);
        }

        int rValue = (int)(r * 255);
        int gValue = (int)(g * 255);
        int bValue = (int)(b * 255);

        return $"rgb({rValue}, {gValue}, {bValue})";
    }

    private static double HueToRgb(double p, double q, double t)
    {
        if (t < 0)
        {
            t += 1;
        }

        if (t > 1)
        {
            t -= 1;
        }

        if (t < 1.0 / 6)
        {
            return p + (q - p) * 6 * t;
        }

        if (t < 1.0 / 2)
        {
            return q;
        }

        if (t < 2.0 / 3)
        {
            return p + (q - p) * (2.0 / 3 - t) * 6;
        }

        return p;
    }

    public override string ToString() => ToCssString();
    public override int GetHashCode() => HashCode.Combine(Value, Type);
}
