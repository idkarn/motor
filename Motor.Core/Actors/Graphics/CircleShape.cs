using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

public class CircleShape : Shape<Circle>
{
    public int Radius
    {
        get => GetModifier<Circle>()!.Radius;
        set => GetModifier<Circle>()!.Radius = value;
    }
}