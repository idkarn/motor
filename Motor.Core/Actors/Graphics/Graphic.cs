using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

public abstract class Graphic<T> : Actor
    where T : VisualModifierBase, new()
{
    public Graphic(bool isEmpty) : base(isEmpty)
    {
        if (!isEmpty)
            AddModifier(new T());
    }

    public Graphic() : this(false) { }
}