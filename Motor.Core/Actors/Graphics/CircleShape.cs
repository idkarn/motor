using Motor.Core.Guards;
using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

[RegisterRole("Circle")]
public class CircleShape : Shape<Circle>
{
    public int Radius
    {
        get => GetModifier<Circle>()!.Radius;
        set => GetModifier<Circle>()!.Radius = value;
    }

    public CircleShape(bool isEmpty) : base(isEmpty) { }
    public CircleShape() : base() { }
}