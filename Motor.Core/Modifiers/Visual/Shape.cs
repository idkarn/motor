using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

public abstract class Shape : VisualModifierBase
{
    protected Color _rayColor;
    public Color16 Color
    {
        get => Color;
        set
        {
            var clr = value.ToColor();
            _rayColor = new Color(clr.R, clr.G, clr.B, clr.A);
        }
    }

    public Shape()
    {
        Color = Color16.Green;
    }
}