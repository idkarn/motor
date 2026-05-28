using System.Numerics;
using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

[Guards.RegisterRole("Rectangle")]
public class RectangleShape : Shape<Rectangle>
{
    public RectangleShape(bool isEmpty) : base(isEmpty) { }
    public RectangleShape() : base() { }
    public Vector2 Size
    {
        get => GetModifier<Rectangle>()!.Size;
        set => GetModifier<Rectangle>()!.Size = value;
    }
}