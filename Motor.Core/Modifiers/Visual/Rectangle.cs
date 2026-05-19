using System.Numerics;
using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

public class Rectangle : Shape
{
    public Vector2 Size { get; set; } = new(20, 20);

    public override void Draw()
    {
        var rect = new Raylib_cs.Rectangle(_transform.WorldPosition - Size / 2, Size);

        if (IsHollow)
            Raylib.DrawRectangleLinesEx(rect, LineWidth, _rayColor);
        else
            Raylib.DrawRectangleRec(rect, _rayColor);
    }
}