using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

public class CircleShape(bool isEmpty) : Shape<Circle>(isEmpty)
{
    public int Radius
    {
        get => GetModifier<Circle>()!.Radius;
        set => GetModifier<Circle>()!.Radius = value;
    }
}