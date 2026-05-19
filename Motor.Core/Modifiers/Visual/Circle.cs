using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

public class Circle : Shape
{
    public int Radius { get; set; } = 20;

    public override void Draw()
    {
        if (IsHollow)
            Raylib.DrawRing(_transform.WorldPosition, Radius - (LineWidth / 2.0f), Radius + (LineWidth / 2.0f), 0.0f, 360.0f, 0, _rayColor);
        else
            Raylib.DrawCircleV(_transform.WorldPosition, Radius, _rayColor);
    }
}