using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

public class Circle : Shape
{
    public int Radius { get; set; } = 20;

    public override void Draw()
    {
        Raylib.DrawCircleV(_transform.WorldPosition, Radius, _rayColor);
    }
}