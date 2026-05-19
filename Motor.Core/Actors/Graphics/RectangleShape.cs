using System.Numerics;
using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

public class RectangleShape : Shape<Rectangle>
{
    public Vector2 Size
    {
        get => GetModifier<Rectangle>()!.Size;
        set => GetModifier<Rectangle>()!.Size = value;
    }
}