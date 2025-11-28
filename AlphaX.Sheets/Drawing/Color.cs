namespace AlphaX.Sheets.Drawing;

public struct Color(byte a, byte r, byte g, byte b) : IEquatable<Color>
{
    private static readonly Dictionary<KnownColor, Color> _colorsCache;

    static Color() => _colorsCache = [];

    public byte A { get; set; } = a;
    public byte R { get; set; } = r;
    public byte G { get; set; } = g;
    public byte B { get; set; } = b;

    public static Color FromArgb(byte a, byte r, byte g, byte b) => new(a, r, g, b);

    public readonly bool Equals(Color color2) => A == color2.A && R == color2.R && G == color2.G && B == color2.B;

    public static bool operator ==(Color color1, Color color2) => color1.Equals(color2);

    public static bool operator !=(Color color1, Color color2) => !color1.Equals(color2);

    #region Colors
    public static Color Transparent
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Transparent, out var color))
            {
                color = FromArgb(0, 255, 255, 255);
                _colorsCache.Add(KnownColor.Transparent, color);
            }

            return color;
        }
    }

    public static Color AliceBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.AliceBlue, out var color))
            {
                color = FromArgb(255, 240, 248, 255);
                _colorsCache.Add(KnownColor.AliceBlue, color);
            }

            return color;
        }
    }

    public static Color AntiqueWhite
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.AntiqueWhite, out var color))
            {
                color = FromArgb(255, 250, 235, 215);
                _colorsCache.Add(KnownColor.AntiqueWhite, color);
            }

            return color;
        }
    }

    public static Color Aqua
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Aqua, out var color))
            {
                color = FromArgb(255, 0, 255, 255);
                _colorsCache.Add(KnownColor.Aqua, color);
            }

            return color;
        }
    }

    public static Color Aquamarine
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Aquamarine, out var color))
            {
                color = FromArgb(255, 127, 255, 212);
                _colorsCache.Add(KnownColor.Aquamarine, color);
            }

            return color;
        }
    }

    public static Color Azure
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Azure, out var color))
            {
                color = FromArgb(255, 240, 255, 255);
                _colorsCache.Add(KnownColor.Azure, color);
            }

            return color;
        }
    }

    public static Color Beige
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Beige, out var color))
            {
                color = FromArgb(255, 245, 245, 220);
                _colorsCache.Add(KnownColor.Beige, color);
            }

            return color;
        }
    }

    public static Color Bisque
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Bisque, out var color))
            {
                color = FromArgb(255, 255, 228, 196);
                _colorsCache.Add(KnownColor.Bisque, color);
            }

            return color;
        }
    }

    public static Color Black
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Black, out var color))
            {
                color = FromArgb(255, 0, 0, 0);
                _colorsCache.Add(KnownColor.Black, color);
            }

            return color;
        }
    }

    public static Color BlanchedAlmond
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.BlanchedAlmond, out var color))
            {
                color = FromArgb(255, 255, 235, 205);
                _colorsCache.Add(KnownColor.BlanchedAlmond, color);
            }

            return color;
        }
    }

    public static Color Blue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Blue, out var color))
            {
                color = FromArgb(255, 0, 0, 255);
                _colorsCache.Add(KnownColor.Blue, color);
            }

            return color;
        }
    }

    public static Color BlueViolet
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.BlueViolet, out var color))
            {
                color = FromArgb(255, 138, 43, 226);
                _colorsCache.Add(KnownColor.BlueViolet, color);
            }

            return color;
        }
    }

    public static Color Brown
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Brown, out var color))
            {
                color = FromArgb(255, 165, 42, 42);
                _colorsCache.Add(KnownColor.Brown, color);
            }

            return color;
        }
    }

    public static Color BurlyWood
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.BurlyWood, out var color))
            {
                color = FromArgb(255, 222, 184, 135);
                _colorsCache.Add(KnownColor.BurlyWood, color);
            }

            return color;
        }
    }

    public static Color CadetBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.CadetBlue, out var color))
            {
                color = FromArgb(255, 95, 158, 160);
                _colorsCache.Add(KnownColor.CadetBlue, color);
            }

            return color;
        }
    }

    public static Color Chartreuse
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Chartreuse, out var color))
            {
                color = FromArgb(255, 127, 255, 0);
                _colorsCache.Add(KnownColor.Chartreuse, color);
            }

            return color;
        }
    }

    public static Color Chocolate
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Chocolate, out var color))
            {
                color = FromArgb(255, 210, 105, 30);
                _colorsCache.Add(KnownColor.Chocolate, color);
            }

            return color;
        }
    }

    public static Color Coral
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Coral, out var color))
            {
                color = FromArgb(255, 255, 127, 80);
                _colorsCache.Add(KnownColor.Coral, color);
            }

            return color;
        }
    }

    public static Color CornflowerBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.CornflowerBlue, out var color))
            {
                color = FromArgb(255, 100, 149, 237);
                _colorsCache.Add(KnownColor.CornflowerBlue, color);
            }

            return color;
        }
    }

    public static Color Cornsilk
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Cornsilk, out var color))
            {
                color = FromArgb(255, 255, 248, 220);
                _colorsCache.Add(KnownColor.Cornsilk, color);
            }

            return color;
        }
    }

    public static Color Crimson
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Crimson, out var color))
            {
                color = FromArgb(255, 220, 20, 60);
                _colorsCache.Add(KnownColor.Crimson, color);
            }

            return color;
        }
    }

    public static Color Cyan
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Cyan, out var color))
            {
                color = FromArgb(255, 0, 255, 255);
                _colorsCache.Add(KnownColor.Cyan, color);
            }

            return color;
        }
    }

    public static Color DarkBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkBlue, out var color))
            {
                color = FromArgb(255, 0, 0, 139);
                _colorsCache.Add(KnownColor.DarkBlue, color);
            }

            return color;
        }
    }

    public static Color DarkCyan
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkCyan, out var color))
            {
                color = FromArgb(255, 0, 139, 139);
                _colorsCache.Add(KnownColor.DarkCyan, color);
            }

            return color;
        }
    }

    public static Color DarkGoldenrod
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkGoldenrod, out var color))
            {
                color = FromArgb(255, 184, 134, 11);
                _colorsCache.Add(KnownColor.DarkGoldenrod, color);
            }

            return color;
        }
    }

    public static Color DarkGray
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkGray, out var color))
            {
                color = FromArgb(255, 169, 169, 169);
                _colorsCache.Add(KnownColor.DarkGray, color);
            }

            return color;
        }
    }

    public static Color DarkGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkGreen, out var color))
            {
                color = FromArgb(255, 0, 100, 0);
                _colorsCache.Add(KnownColor.DarkGreen, color);
            }

            return color;
        }
    }

    public static Color DarkKhaki
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkKhaki, out var color))
            {
                color = FromArgb(255, 189, 183, 107);
                _colorsCache.Add(KnownColor.DarkKhaki, color);
            }

            return color;
        }
    }

    public static Color DarkMagenta
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkMagenta, out var color))
            {
                color = FromArgb(255, 139, 0, 139);
                _colorsCache.Add(KnownColor.DarkMagenta, color);
            }

            return color;
        }
    }

    public static Color DarkOliveGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkOliveGreen, out var color))
            {
                color = FromArgb(255, 85, 107, 47);
                _colorsCache.Add(KnownColor.DarkOliveGreen, color);
            }

            return color;
        }
    }

    public static Color DarkOrange
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkOrange, out var color))
            {
                color = FromArgb(255, 255, 140, 0);
                _colorsCache.Add(KnownColor.DarkOrange, color);
            }

            return color;
        }
    }

    public static Color DarkOrchid
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkOrchid, out var color))
            {
                color = FromArgb(255, 153, 50, 204);
                _colorsCache.Add(KnownColor.DarkOrchid, color);
            }

            return color;
        }
    }

    public static Color DarkRed
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkRed, out var color))
            {
                color = FromArgb(255, 139, 0, 0);
                _colorsCache.Add(KnownColor.DarkRed, color);
            }

            return color;
        }
    }

    public static Color DarkSalmon
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkSalmon, out var color))
            {
                color = FromArgb(255, 233, 150, 122);
                _colorsCache.Add(KnownColor.DarkSalmon, color);
            }

            return color;
        }
    }

    public static Color DarkSeaGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkSeaGreen, out var color))
            {
                color = FromArgb(255, 143, 188, 139);
                _colorsCache.Add(KnownColor.DarkSeaGreen, color);
            }

            return color;
        }
    }

    public static Color DarkSlateBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkSlateBlue, out var color))
            {
                color = FromArgb(255, 72, 61, 139);
                _colorsCache.Add(KnownColor.DarkSlateBlue, color);
            }

            return color;
        }
    }

    public static Color DarkSlateGray
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkSlateGray, out var color))
            {
                color = FromArgb(255, 47, 79, 79);
                _colorsCache.Add(KnownColor.DarkSlateGray, color);
            }

            return color;
        }
    }

    public static Color DarkTurquoise
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkTurquoise, out var color))
            {
                color = FromArgb(255, 0, 206, 209);
                _colorsCache.Add(KnownColor.DarkTurquoise, color);
            }

            return color;
        }
    }

    public static Color DarkViolet
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DarkViolet, out var color))
            {
                color = FromArgb(255, 148, 0, 211);
                _colorsCache.Add(KnownColor.DarkViolet, color);
            }

            return color;
        }
    }

    public static Color DeepPink
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DeepPink, out var color))
            {
                color = FromArgb(255, 255, 20, 147);
                _colorsCache.Add(KnownColor.DeepPink, color);
            }

            return color;
        }
    }

    public static Color DeepSkyBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DeepSkyBlue, out var color))
            {
                color = FromArgb(255, 0, 191, 255);
                _colorsCache.Add(KnownColor.DeepSkyBlue, color);
            }

            return color;
        }
    }

    public static Color DimGray
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DimGray, out var color))
            {
                color = FromArgb(255, 105, 105, 105);
                _colorsCache.Add(KnownColor.DimGray, color);
            }

            return color;
        }
    }

    public static Color DodgerBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.DodgerBlue, out var color))
            {
                color = FromArgb(255, 30, 144, 255);
                _colorsCache.Add(KnownColor.DodgerBlue, color);
            }

            return color;
        }
    }

    public static Color Firebrick
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Firebrick, out var color))
            {
                color = FromArgb(255, 178, 34, 34);
                _colorsCache.Add(KnownColor.Firebrick, color);
            }

            return color;
        }
    }

    public static Color FloralWhite
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.FloralWhite, out var color))
            {
                color = FromArgb(255, 255, 250, 240);
                _colorsCache.Add(KnownColor.FloralWhite, color);
            }

            return color;
        }
    }

    public static Color ForestGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.ForestGreen, out var color))
            {
                color = FromArgb(255, 34, 139, 34);
                _colorsCache.Add(KnownColor.ForestGreen, color);
            }

            return color;
        }
    }

    public static Color Fuchsia
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Fuchsia, out var color))
            {
                color = FromArgb(255, 255, 0, 255);
                _colorsCache.Add(KnownColor.Fuchsia, color);
            }

            return color;
        }
    }

    public static Color Gainsboro
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Gainsboro, out var color))
            {
                color = FromArgb(255, 220, 220, 220);
                _colorsCache.Add(KnownColor.Gainsboro, color);
            }

            return color;
        }
    }

    public static Color GhostWhite
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.GhostWhite, out var color))
            {
                color = FromArgb(255, 248, 248, 255);
                _colorsCache.Add(KnownColor.GhostWhite, color);
            }

            return color;
        }
    }

    public static Color Gold
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Gold, out var color))
            {
                color = FromArgb(255, 255, 215, 0);
                _colorsCache.Add(KnownColor.Gold, color);
            }

            return color;
        }
    }

    public static Color Goldenrod
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Goldenrod, out var color))
            {
                color = FromArgb(255, 218, 165, 32);
                _colorsCache.Add(KnownColor.Goldenrod, color);
            }

            return color;
        }
    }

    public static Color Gray
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Gray, out var color))
            {
                color = FromArgb(255, 128, 128, 128);
                _colorsCache.Add(KnownColor.Gray, color);
            }

            return color;
        }
    }

    public static Color Green
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Green, out var color))
            {
                color = FromArgb(255, 0, 128, 0);
                _colorsCache.Add(KnownColor.Green, color);
            }

            return color;
        }
    }

    public static Color GreenYellow
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.GreenYellow, out var color))
            {
                color = FromArgb(255, 173, 255, 47);
                _colorsCache.Add(KnownColor.GreenYellow, color);
            }

            return color;
        }
    }

    public static Color Honeydew
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Honeydew, out var color))
            {
                color = FromArgb(255, 240, 255, 240);
                _colorsCache.Add(KnownColor.Honeydew, color);
            }

            return color;
        }
    }

    public static Color HotPink
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.HotPink, out var color))
            {
                color = FromArgb(255, 255, 105, 180);
                _colorsCache.Add(KnownColor.HotPink, color);
            }

            return color;
        }
    }

    public static Color IndianRed
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.IndianRed, out var color))
            {
                color = FromArgb(255, 205, 92, 92);
                _colorsCache.Add(KnownColor.IndianRed, color);
            }

            return color;
        }
    }

    public static Color Indigo
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Indigo, out var color))
            {
                color = FromArgb(255, 75, 0, 130);
                _colorsCache.Add(KnownColor.Indigo, color);
            }

            return color;
        }
    }

    public static Color Ivory
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Ivory, out var color))
            {
                color = FromArgb(255, 255, 255, 240);
                _colorsCache.Add(KnownColor.Ivory, color);
            }

            return color;
        }
    }

    public static Color Khaki
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Khaki, out var color))
            {
                color = FromArgb(255, 240, 230, 140);
                _colorsCache.Add(KnownColor.Khaki, color);
            }

            return color;
        }
    }

    public static Color Lavender
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Lavender, out var color))
            {
                color = FromArgb(255, 230, 230, 250);
                _colorsCache.Add(KnownColor.Lavender, color);
            }

            return color;
        }
    }

    public static Color LavenderBlush
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LavenderBlush, out var color))
            {
                color = FromArgb(255, 255, 240, 245);
                _colorsCache.Add(KnownColor.LavenderBlush, color);
            }

            return color;
        }
    }

    public static Color LawnGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LawnGreen, out var color))
            {
                color = FromArgb(255, 124, 252, 0);
                _colorsCache.Add(KnownColor.LawnGreen, color);
            }

            return color;
        }
    }

    public static Color LemonChiffon
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LemonChiffon, out var color))
            {
                color = FromArgb(255, 255, 250, 205);
                _colorsCache.Add(KnownColor.LemonChiffon, color);
            }

            return color;
        }
    }

    public static Color LightBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightBlue, out var color))
            {
                color = FromArgb(255, 173, 216, 230);
                _colorsCache.Add(KnownColor.LightBlue, color);
            }

            return color;
        }
    }

    public static Color LightCoral
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightCoral, out var color))
            {
                color = FromArgb(255, 240, 128, 128);
                _colorsCache.Add(KnownColor.LightCoral, color);
            }

            return color;
        }
    }

    public static Color LightCyan
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightCyan, out var color))
            {
                color = FromArgb(255, 224, 255, 255);
                _colorsCache.Add(KnownColor.LightCyan, color);
            }

            return color;
        }
    }

    public static Color LightGoldenrodYellow
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightGoldenrodYellow, out var color))
            {
                color = FromArgb(255, 250, 250, 210);
                _colorsCache.Add(KnownColor.LightGoldenrodYellow, color);
            }

            return color;
        }
    }

    public static Color LightGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightGreen, out var color))
            {
                color = FromArgb(255, 144, 238, 144);
                _colorsCache.Add(KnownColor.LightGreen, color);
            }

            return color;
        }
    }

    public static Color LightGray
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightGray, out var color))
            {
                color = FromArgb(255, 211, 211, 211);
                _colorsCache.Add(KnownColor.LightGray, color);
            }

            return color;
        }
    }

    public static Color LightPink
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightPink, out var color))
            {
                color = FromArgb(255, 255, 182, 193);
                _colorsCache.Add(KnownColor.LightPink, color);
            }

            return color;
        }
    }

    public static Color LightSalmon
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightSalmon, out var color))
            {
                color = FromArgb(255, 255, 160, 122);
                _colorsCache.Add(KnownColor.LightSalmon, color);
            }

            return color;
        }
    }

    public static Color LightSeaGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightSeaGreen, out var color))
            {
                color = FromArgb(255, 32, 178, 170);
                _colorsCache.Add(KnownColor.LightSeaGreen, color);
            }

            return color;
        }
    }

    public static Color LightSkyBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightSkyBlue, out var color))
            {
                color = FromArgb(255, 135, 206, 250);
                _colorsCache.Add(KnownColor.LightSkyBlue, color);
            }

            return color;
        }
    }

    public static Color LightSlateGray
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightSlateGray, out var color))
            {
                color = FromArgb(255, 119, 136, 153);
                _colorsCache.Add(KnownColor.LightSlateGray, color);
            }

            return color;
        }
    }

    public static Color LightSteelBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightSteelBlue, out var color))
            {
                color = FromArgb(255, 176, 196, 222);
                _colorsCache.Add(KnownColor.LightSteelBlue, color);
            }

            return color;
        }
    }

    public static Color LightYellow
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LightYellow, out var color))
            {
                color = FromArgb(255, 255, 255, 224);
                _colorsCache.Add(KnownColor.LightYellow, color);
            }

            return color;
        }
    }

    public static Color Lime
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Lime, out var color))
            {
                color = FromArgb(255, 0, 255, 0);
                _colorsCache.Add(KnownColor.Lime, color);
            }

            return color;
        }
    }

    public static Color LimeGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.LimeGreen, out var color))
            {
                color = FromArgb(255, 50, 205, 50);
                _colorsCache.Add(KnownColor.LimeGreen, color);
            }

            return color;
        }
    }

    public static Color Linen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Linen, out var color))
            {
                color = FromArgb(255, 250, 240, 230);
                _colorsCache.Add(KnownColor.Linen, color);
            }

            return color;
        }
    }

    public static Color Magenta
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Magenta, out var color))
            {
                color = FromArgb(255, 255, 0, 255);
                _colorsCache.Add(KnownColor.Magenta, color);
            }

            return color;
        }
    }

    public static Color Maroon
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Maroon, out var color))
            {
                color = FromArgb(255, 128, 0, 0);
                _colorsCache.Add(KnownColor.Maroon, color);
            }

            return color;
        }
    }

    public static Color MediumAquamarine
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MediumAquamarine, out var color))
            {
                color = FromArgb(255, 102, 205, 170);
                _colorsCache.Add(KnownColor.MediumAquamarine, color);
            }

            return color;
        }
    }

    public static Color MediumBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MediumBlue, out var color))
            {
                color = FromArgb(255, 0, 0, 205);
                _colorsCache.Add(KnownColor.MediumBlue, color);
            }

            return color;
        }
    }

    public static Color MediumOrchid
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MediumOrchid, out var color))
            {
                color = FromArgb(255, 186, 85, 211);
                _colorsCache.Add(KnownColor.MediumOrchid, color);
            }

            return color;
        }
    }

    public static Color MediumPurple
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MediumPurple, out var color))
            {
                color = FromArgb(255, 147, 112, 219);
                _colorsCache.Add(KnownColor.MediumPurple, color);
            }

            return color;
        }
    }

    public static Color MediumSeaGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MediumSeaGreen, out var color))
            {
                color = FromArgb(255, 60, 179, 113);
                _colorsCache.Add(KnownColor.MediumSeaGreen, color);
            }

            return color;
        }
    }

    public static Color MediumSlateBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MediumSlateBlue, out var color))
            {
                color = FromArgb(255, 123, 104, 238);
                _colorsCache.Add(KnownColor.MediumSlateBlue, color);
            }

            return color;
        }
    }

    public static Color MediumSpringGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MediumSpringGreen, out var color))
            {
                color = FromArgb(255, 0, 250, 154);
                _colorsCache.Add(KnownColor.MediumSpringGreen, color);
            }

            return color;
        }
    }

    public static Color MediumTurquoise
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MediumTurquoise, out var color))
            {
                color = FromArgb(255, 72, 209, 204);
                _colorsCache.Add(KnownColor.MediumTurquoise, color);
            }

            return color;
        }
    }

    public static Color MediumVioletRed
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MediumVioletRed, out var color))
            {
                color = FromArgb(255, 199, 21, 133);
                _colorsCache.Add(KnownColor.MediumVioletRed, color);
            }

            return color;
        }
    }

    public static Color MidnightBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MidnightBlue, out var color))
            {
                color = FromArgb(255, 25, 25, 112);
                _colorsCache.Add(KnownColor.MidnightBlue, color);
            }

            return color;
        }
    }

    public static Color MintCream
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MintCream, out var color))
            {
                color = FromArgb(255, 245, 255, 250);
                _colorsCache.Add(KnownColor.MintCream, color);
            }

            return color;
        }
    }

    public static Color MistyRose
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.MistyRose, out var color))
            {
                color = FromArgb(255, 255, 228, 225);
                _colorsCache.Add(KnownColor.MistyRose, color);
            }

            return color;
        }
    }

    public static Color Moccasin
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Moccasin, out var color))
            {
                color = FromArgb(255, 255, 228, 181);
                _colorsCache.Add(KnownColor.Moccasin, color);
            }

            return color;
        }
    }

    public static Color NavajoWhite
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.NavajoWhite, out var color))
            {
                color = FromArgb(255, 255, 222, 173);
                _colorsCache.Add(KnownColor.NavajoWhite, color);
            }

            return color;
        }
    }

    public static Color Navy
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Navy, out var color))
            {
                color = FromArgb(255, 0, 0, 128);
                _colorsCache.Add(KnownColor.Navy, color);
            }

            return color;
        }
    }

    public static Color OldLace
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.OldLace, out var color))
            {
                color = FromArgb(255, 253, 245, 230);
                _colorsCache.Add(KnownColor.OldLace, color);
            }

            return color;
        }
    }

    public static Color Olive
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Olive, out var color))
            {
                color = FromArgb(255, 128, 128, 0);
                _colorsCache.Add(KnownColor.Olive, color);
            }

            return color;
        }
    }

    public static Color OliveDrab
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.OliveDrab, out var color))
            {
                color = FromArgb(255, 107, 142, 35);
                _colorsCache.Add(KnownColor.OliveDrab, color);
            }

            return color;
        }
    }

    public static Color Orange
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Orange, out var color))
            {
                color = FromArgb(255, 255, 165, 0);
                _colorsCache.Add(KnownColor.Orange, color);
            }

            return color;
        }
    }

    public static Color OrangeRed
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.OrangeRed, out var color))
            {
                color = FromArgb(255, 255, 69, 0);
                _colorsCache.Add(KnownColor.OrangeRed, color);
            }

            return color;
        }
    }

    public static Color Orchid
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Orchid, out var color))
            {
                color = FromArgb(255, 218, 112, 214);
                _colorsCache.Add(KnownColor.Orchid, color);
            }

            return color;
        }
    }

    public static Color PaleGoldenrod
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.PaleGoldenrod, out var color))
            {
                color = FromArgb(255, 238, 232, 170);
                _colorsCache.Add(KnownColor.PaleGoldenrod, color);
            }

            return color;
        }
    }

    public static Color PaleGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.PaleGreen, out var color))
            {
                color = FromArgb(255, 152, 251, 152);
                _colorsCache.Add(KnownColor.PaleGreen, color);
            }

            return color;
        }
    }

    public static Color PaleTurquoise
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.PaleTurquoise, out var color))
            {
                color = FromArgb(255, 175, 238, 238);
                _colorsCache.Add(KnownColor.PaleTurquoise, color);
            }

            return color;
        }
    }

    public static Color PaleVioletRed
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.PaleVioletRed, out var color))
            {
                color = FromArgb(255, 219, 112, 147);
                _colorsCache.Add(KnownColor.PaleVioletRed, color);
            }

            return color;
        }
    }

    public static Color PapayaWhip
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.PapayaWhip, out var color))
            {
                color = FromArgb(255, 255, 239, 213);
                _colorsCache.Add(KnownColor.PapayaWhip, color);
            }

            return color;
        }
    }

    public static Color PeachPuff
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.PeachPuff, out var color))
            {
                color = FromArgb(255, 255, 218, 185);
                _colorsCache.Add(KnownColor.PeachPuff, color);
            }

            return color;
        }
    }

    public static Color Peru
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Peru, out var color))
            {
                color = FromArgb(255, 205, 133, 63);
                _colorsCache.Add(KnownColor.Peru, color);
            }

            return color;
        }
    }

    public static Color Pink
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Pink, out var color))
            {
                color = FromArgb(255, 255, 192, 203);
                _colorsCache.Add(KnownColor.Pink, color);
            }

            return color;
        }
    }

    public static Color Plum
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Plum, out var color))
            {
                color = FromArgb(255, 221, 160, 221);
                _colorsCache.Add(KnownColor.Plum, color);
            }

            return color;
        }
    }

    public static Color PowderBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.PowderBlue, out var color))
            {
                color = FromArgb(255, 176, 224, 230);
                _colorsCache.Add(KnownColor.PowderBlue, color);
            }

            return color;
        }
    }

    public static Color Purple
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Purple, out var color))
            {
                color = FromArgb(255, 128, 0, 128);
                _colorsCache.Add(KnownColor.Purple, color);
            }

            return color;
        }
    }

    public static Color Red
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Red, out var color))
            {
                color = FromArgb(255, 255, 0, 0);
                _colorsCache.Add(KnownColor.Red, color);
            }

            return color;
        }
    }

    public static Color RosyBrown
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.RosyBrown, out var color))
            {
                color = FromArgb(255, 188, 143, 143);
                _colorsCache.Add(KnownColor.RosyBrown, color);
            }

            return color;
        }
    }

    public static Color RoyalBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.RoyalBlue, out var color))
            {
                color = FromArgb(255, 65, 105, 225);
                _colorsCache.Add(KnownColor.RoyalBlue, color);
            }

            return color;
        }
    }

    public static Color SaddleBrown
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.SaddleBrown, out var color))
            {
                color = FromArgb(255, 139, 69, 19);
                _colorsCache.Add(KnownColor.SaddleBrown, color);
            }

            return color;
        }
    }

    public static Color Salmon
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Salmon, out var color))
            {
                color = FromArgb(255, 250, 128, 114);
                _colorsCache.Add(KnownColor.Salmon, color);
            }

            return color;
        }
    }

    public static Color SandyBrown
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.SandyBrown, out var color))
            {
                color = FromArgb(255, 244, 164, 96);
                _colorsCache.Add(KnownColor.SandyBrown, color);
            }

            return color;
        }
    }

    public static Color SeaGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.SeaGreen, out var color))
            {
                color = FromArgb(255, 46, 139, 87);
                _colorsCache.Add(KnownColor.SeaGreen, color);
            }

            return color;
        }
    }

    public static Color SeaShell
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.SeaShell, out var color))
            {
                color = FromArgb(255, 255, 245, 238);
                _colorsCache.Add(KnownColor.SeaShell, color);
            }

            return color;
        }
    }

    public static Color Sienna
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Sienna, out var color))
            {
                color = FromArgb(255, 160, 82, 45);
                _colorsCache.Add(KnownColor.Sienna, color);
            }

            return color;
        }
    }

    public static Color Silver
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Silver, out var color))
            {
                color = FromArgb(255, 192, 192, 192);
                _colorsCache.Add(KnownColor.Silver, color);
            }

            return color;
        }
    }

    public static Color SkyBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.SkyBlue, out var color))
            {
                color = FromArgb(255, 135, 206, 235);
                _colorsCache.Add(KnownColor.SkyBlue, color);
            }

            return color;
        }
    }

    public static Color SlateBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.SlateBlue, out var color))
            {
                color = FromArgb(255, 106, 90, 205);
                _colorsCache.Add(KnownColor.SlateBlue, color);
            }

            return color;
        }
    }

    public static Color SlateGray
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.SlateGray, out var color))
            {
                color = FromArgb(255, 112, 128, 144);
                _colorsCache.Add(KnownColor.SlateGray, color);
            }

            return color;
        }
    }

    public static Color Snow
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Snow, out var color))
            {
                color = FromArgb(255, 255, 250, 250);
                _colorsCache.Add(KnownColor.Snow, color);
            }

            return color;
        }
    }

    public static Color SpringGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.SpringGreen, out var color))
            {
                color = FromArgb(255, 0, 255, 127);
                _colorsCache.Add(KnownColor.SpringGreen, color);
            }

            return color;
        }
    }

    public static Color SteelBlue
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.SteelBlue, out var color))
            {
                color = FromArgb(255, 70, 130, 180);
                _colorsCache.Add(KnownColor.SteelBlue, color);
            }

            return color;
        }
    }

    public static Color Tan
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Tan, out var color))
            {
                color = FromArgb(255, 210, 180, 140);
                _colorsCache.Add(KnownColor.Tan, color);
            }

            return color;
        }
    }

    public static Color Teal
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Teal, out var color))
            {
                color = FromArgb(255, 0, 128, 128);
                _colorsCache.Add(KnownColor.Teal, color);
            }

            return color;
        }
    }

    public static Color Thistle
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Thistle, out var color))
            {
                color = FromArgb(255, 216, 191, 216);
                _colorsCache.Add(KnownColor.Thistle, color);
            }

            return color;
        }
    }

    public static Color Tomato
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Tomato, out var color))
            {
                color = FromArgb(255, 255, 99, 71);
                _colorsCache.Add(KnownColor.Tomato, color);
            }

            return color;
        }
    }

    public static Color Turquoise
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Turquoise, out var color))
            {
                color = FromArgb(255, 64, 224, 208);
                _colorsCache.Add(KnownColor.Turquoise, color);
            }

            return color;
        }
    }

    public static Color Violet
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Violet, out var color))
            {
                color = FromArgb(255, 238, 130, 238);
                _colorsCache.Add(KnownColor.Violet, color);
            }

            return color;
        }
    }

    public static Color Wheat
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Wheat, out var color))
            {
                color = FromArgb(255, 245, 222, 179);
                _colorsCache.Add(KnownColor.Wheat, color);
            }

            return color;
        }
    }

    public static Color White
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.White, out var color))
            {
                color = FromArgb(255, 255, 255, 255);
                _colorsCache.Add(KnownColor.White, color);
            }

            return color;
        }
    }

    public static Color WhiteSmoke
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.WhiteSmoke, out var color))
            {
                color = FromArgb(255, 245, 245, 245);
                _colorsCache.Add(KnownColor.WhiteSmoke, color);
            }

            return color;
        }
    }

    public static Color Yellow
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.Yellow, out var color))
            {
                color = FromArgb(255, 255, 255, 0);
                _colorsCache.Add(KnownColor.Yellow, color);
            }

            return color;
        }
    }

    public static Color YellowGreen
    {
        get
        {
            if (!_colorsCache.TryGetValue(KnownColor.YellowGreen, out var color))
            {
                color = FromArgb(255, 154, 205, 50);
                _colorsCache.Add(KnownColor.YellowGreen, color);
            }

            return color;
        }
    }
    #endregion
}
