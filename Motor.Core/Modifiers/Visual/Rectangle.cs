using System.Numerics;
using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

public class Rectangle : Shape
{
    public Vector2 Size { get; set; } = new(20, 20);

    public override void Draw()
    {
        Raylib.DrawRectangleV(_transform.WorldPosition, Size, _rayColor);
    }
}