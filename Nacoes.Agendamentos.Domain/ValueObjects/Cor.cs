namespace Nacoes.Agendamentos.Domain.ValueObjects;

public enum ETipoCor
{
    Hex = 0,
    Rgb = 1,
    Hsl = 2
}

public sealed record class Cor
{
    public string Valor { get; }
    public ETipoCor Tipo { get; }

    public Cor(string valor, ETipoCor tipo)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ArgumentException("Valor da cor não pode ser vazio.", nameof(valor));
        }

        Valor = valor;
        Tipo = tipo;
    }

    public static Cor Default => new("#1F77B4", ETipoCor.Hex);

    public string ToCssString()
    {
        return Tipo switch
        {
            ETipoCor.Hex => NormalizarHex(Valor),
            ETipoCor.Rgb => NormalizarRgb(Valor),
            ETipoCor.Hsl => NormalizarHsl(Valor),
            _ => Valor
        };
    }

    private static string NormalizarHex(string hex)
    {
        if (!hex.StartsWith($"#")) hex = "#" + hex;
        if (hex.Length != 7) throw new ArgumentException("Formato Hex inválido.");
        return hex.ToUpperInvariant();
    }

    private static string NormalizarRgb(string rgb)
    {
        var valores = rgb.Replace("rgb(", "").Replace(")", "").Split(',');
        if (valores.Length != 3) throw new ArgumentException("Formato RGB inválido.");
        int r = int.Parse(valores[0].Trim());
        int g = int.Parse(valores[1].Trim());
        int b = int.Parse(valores[2].Trim());
        return $"rgb({r}, {g}, {b})";
    }

    private static string NormalizarHsl(string hsl)
    {
        var valores = hsl.Replace("hsl(", "").Replace(")", "").Replace("%", "").Split(',');
        if (valores.Length != 3) throw new ArgumentException("Formato HSL inválido.");
        int h = int.Parse(valores[0].Trim());
        int s = int.Parse(valores[1].Trim());
        int l = int.Parse(valores[2].Trim());
        return $"hsl({h}, {s}%, {l}%)";
    }
    
    public Cor ConverterPara(ETipoCor novoTipo)
    {
        if (novoTipo == Tipo)
        {
            return this;
        }

        return novoTipo switch
        {
            ETipoCor.Hex => ConverterParaHex(),
            ETipoCor.Rgb => ConverterParaRgb(),
            ETipoCor.Hsl => ConverterParaHsl(),
            _ => throw new NotSupportedException($"Conversão para o tipo {novoTipo} não suportada.")
        };
    }

    private Cor ConverterParaHex()
    {
        return Tipo switch
        {
            ETipoCor.Rgb => new Cor(RgbParaHex(Valor), ETipoCor.Hex),
            ETipoCor.Hsl => new Cor(RgbParaHex(HslParaRgb(Valor)), ETipoCor.Hex),
            _ => throw new NotSupportedException("Conversão para Hex não suportada para este tipo.")
        };
    }

    private Cor ConverterParaRgb()
    {
        return Tipo switch
        {
            ETipoCor.Hex => new Cor(HexParaRgb(Valor), ETipoCor.Rgb),
            ETipoCor.Hsl => new Cor(HslParaRgb(Valor), ETipoCor.Rgb),
            _ => throw new NotSupportedException("Conversão para Rgb não suportada para este tipo.")
        };
    }

    private Cor ConverterParaHsl()
    {
        return Tipo switch
        {
            ETipoCor.Rgb => new Cor(RgbParaHsl(Valor), ETipoCor.Hsl),
            ETipoCor.Hex => new Cor(RgbParaHsl(HexParaRgb(Valor)), ETipoCor.Hsl),
            _ => throw new NotSupportedException("Conversão para Hsl não suportada para este tipo.")
        };
    }

    // Conversão RGB string "rgb(255, 0, 0)" -> Hex "#FF0000"
    private static string RgbParaHex(string rgb)
    {
        var valores = rgb.Replace("rgb(", "").Replace(")", "").Split(',');
        if (valores.Length != 3)
        {
            throw new ArgumentException("Formato RGB inválido.");
        }

        int r = int.Parse(valores[0].Trim());
        int g = int.Parse(valores[1].Trim());
        int b = int.Parse(valores[2].Trim());

        return $"#{r:X2}{g:X2}{b:X2}";
    }

    // Conversão Hex "#RRGGBB" -> RGB "rgb(R, G, B)"
    private static string HexParaRgb(string hex)
    {
        if (hex.StartsWith("#"))
        {
            hex = hex[1..];
        }

        if (hex.Length != 6)
        {
            throw new ArgumentException("Formato Hex inválido.");
        }

        var r = Convert.ToInt32(hex.Substring(0, 2), 16);
        var g = Convert.ToInt32(hex.Substring(2, 2), 16);
        var b = Convert.ToInt32(hex.Substring(4, 2), 16);

        return $"rgb({r}, {g}, {b})";
    }

    // Conversão RGB "rgb(R, G, B)" -> HSL "hsl(H, S%, L%)"
    private static string RgbParaHsl(string rgb)
    {
        var valores = rgb.Replace("rgb(", "").Replace(")", "").Split(',');
        if (valores.Length != 3)
        {
            throw new ArgumentException("Formato RGB inválido.");
        }

        double r = int.Parse(valores[0].Trim()) / 255.0;
        double g = int.Parse(valores[1].Trim()) / 255.0;
        double b = int.Parse(valores[2].Trim()) / 255.0;

        double max = Math.Max(r, Math.Max(g, b));
        double min = Math.Min(r, Math.Min(g, b));
        double h = 0, s = 0, l = (max + min) / 2.0;

        if (max != min)
        {
            double d = max - min;
            s = l > 0.5 ? d / (2.0 - max - min) : d / (max + min);

            if (max == r)
                h = (g - b) / d + (g < b ? 6 : 0);
            else if (max == g)
                h = (b - r) / d + 2;
            else
                h = (r - g) / d + 4;

            h /= 6;
        }

        int H = (int)(h * 360);
        int S = (int)(s * 100);
        int L = (int)(l * 100);

        return $"hsl({H}, {S}%, {L}%)";
    }

    // Conversão HSL "hsl(H, S%, L%)" -> RGB "rgb(R, G, B)"
    private static string HslParaRgb(string hsl)
    {
        var valores = hsl.Replace("hsl(", "").Replace(")", "").Replace("%", "").Split(',');
        if (valores.Length != 3)
        {
            throw new ArgumentException("Formato HSL inválido.");
        }

        double h = double.Parse(valores[0].Trim()) / 360.0;
        double s = double.Parse(valores[1].Trim()) / 100.0;
        double l = double.Parse(valores[2].Trim()) / 100.0;

        double r, g, b;

        if (s == 0)
            r = g = b = l; // cinza
        else
        {
            double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
            double p = 2 * l - q;
            r = HueParaRgb(p, q, h + 1.0 / 3);
            g = HueParaRgb(p, q, h);
            b = HueParaRgb(p, q, h - 1.0 / 3);
        }

        int R = (int)(r * 255);
        int G = (int)(g * 255);
        int B = (int)(b * 255);

        return $"rgb({R}, {G}, {B})";
    }

    private static double HueParaRgb(double p, double q, double t)
    {
        if (t < 0) t += 1;
        if (t > 1) t -= 1;
        if (t < 1.0 / 6) return p + (q - p) * 6 * t;
        if (t < 1.0 / 2) return q;
        if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
        return p;
    }

    public override string ToString() => ToCssString();
    public override int GetHashCode() => HashCode.Combine(Valor, Tipo);
}
