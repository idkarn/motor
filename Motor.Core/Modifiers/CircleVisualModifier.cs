using Raylib_cs;

namespace Motor.Core.Modifiers;

public class CircleVisualModifier : ShapeVisualModifier
{
    public int Radius { get; set; } = 20;

    public override void Draw()
    {
        Console.WriteLine($"draw {_transform.Position} {Radius} {_rayColor}");
        Raylib.DrawCircleV(_transform.Position, Radius, _rayColor);
    }
}