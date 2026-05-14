using System.Numerics;
using Raylib_cs;

namespace Motor.Core.Modifiers;

public class RectangleVisualModifier : ShapeVisualModifier
{
    public Vector2 Size { get; set; } = new(20, 20);

    public override void Draw()
    {
        Raylib.DrawRectangleV(_transform.Position, Size, _rayColor);
    }
}