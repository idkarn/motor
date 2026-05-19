using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

public abstract class Shape<T> : Graphic<T>
    where T : Shape, new()
{
    public Color16 Color
    {
        get => GetModifier<Shape>()!.Color;
        set => GetModifier<Shape>()!.Color = value;
    }
    public bool IsHollow
    {
        get => GetModifier<Shape>()!.IsHollow;
        set => GetModifier<Shape>()!.IsHollow = value;
    }
    public int LineWidth
    {
        get => GetModifier<Shape>()!.LineWidth;
        set => GetModifier<Shape>()!.LineWidth = value;
    }
}