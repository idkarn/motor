using System.Numerics;

namespace Motor.Core.Modifiers.Area;

public class Rectangle : Area2d
{
    public Vector2 Size { get; set; } = new(20, 20);

    public override bool IsMouseOver()
    {
        return Input.InputManager.IsMouseOver(new Raylib_cs.Rectangle(_transform.WorldPosition - Size / 2, Size));
    }
}