using System.Drawing;

namespace Motor.Core;

// rebuild this with cached rayColor prop
public enum Color16
{
    Black = 0,
    DarkBlue = 1,
    DarkPurple = 2,
    DarkGreen = 3,
    Brown = 4,
    DarkGrey = 5,
    LightGrey = 6,
    White = 7,
    Red = 8,
    Orange = 9,
    Yellow = 10,
    Green = 11,
    Blue = 12,
    LightPurple = 13,
    Pink = 14,
    Peach = 15
}

public static class Palette16
{
    // PICO-8 official 16-color palette (hex)
    private static readonly IReadOnlyDictionary<Color16, Color> _map = new Dictionary<Color16, Color>
    {
        { Color16.Black,       ColorTranslator.FromHtml("#000000") },
        { Color16.DarkBlue,    ColorTranslator.FromHtml("#1D2B53") },
        { Color16.DarkPurple,  ColorTranslator.FromHtml("#7E2553") },
        { Color16.DarkGreen,   ColorTranslator.FromHtml("#008751") },
        { Color16.Brown,       ColorTranslator.FromHtml("#AB5236") },
        { Color16.DarkGrey,    ColorTranslator.FromHtml("#5F574F") },
        { Color16.LightGrey,   ColorTranslator.FromHtml("#C2C3C7") },
        { Color16.White,       ColorTranslator.FromHtml("#FFF1E8") },
        { Color16.Red,         ColorTranslator.FromHtml("#FF004D") },
        { Color16.Orange,      ColorTranslator.FromHtml("#FFA300") },
        { Color16.Yellow,      ColorTranslator.FromHtml("#FFEC27") },
        { Color16.Green,       ColorTranslator.FromHtml("#00E436") },
        { Color16.Blue,        ColorTranslator.FromHtml("#29ADFF") },
        { Color16.LightPurple, ColorTranslator.FromHtml("#83769C") },
        { Color16.Pink,        ColorTranslator.FromHtml("#FF77A8") },
        { Color16.Peach,       ColorTranslator.FromHtml("#FFCCAA") }
    };

    public static Color ToColor(this Color16 p) => _map[p];

    public static Color16 Nearest(Color c)
    {
        static double BestDist(Color a, Color b)
        {
            var dr = a.R - b.R;
            var dg = a.G - b.G;
            var db = a.B - b.B;
            return dr * dr + dg * dg + db * db;
        }

        return _map
            .OrderBy(kv => BestDist(kv.Value, c))
            .First().Key;
    }

    public static Color ToNearestPico8Color(this Color c) => ToColor(Nearest(c));
}
