using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

public class Text : VisualModifierBase
{
    public string Value { get; set; } = "";
    public int FontSize { get; set; } = 5;
    protected Color _rayColor;
    Color16 _color;
    public Color16 Color
    {
        get => _color;
        set
        {
            var clr = value.ToColor();
            _rayColor = new Color(clr.R, clr.G, clr.B, clr.A);
        }
    }

    public override void Draw()
    {
        Raylib.DrawTextEx(Screen.Font, Value, _transform.Position - Raylib.MeasureTextEx(Screen.Font, Value, FontSize, 1) / 2, FontSize, 1, _rayColor);
    }

    public Text()
    {
        Color = Color16.Green;
    }
}