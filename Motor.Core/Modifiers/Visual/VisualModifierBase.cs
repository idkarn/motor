using Motor.Core.Guards;

namespace Motor.Core.Modifiers.Visual;

public abstract class VisualModifierBase : ModifierBase
{
    [InjectModifier]
    protected Transform2dModifier _transform = null!;

    public virtual void Draw()
    {
        throw new NotImplementedException();
    }
}