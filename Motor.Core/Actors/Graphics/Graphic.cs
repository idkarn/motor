using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

public abstract class Graphic<T> : Actor
    where T : VisualModifierBase, new()
{
    public Graphic()
    {
        AddModifier(new T());
    }
}