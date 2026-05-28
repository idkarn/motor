using Motor.Core.Guards;

namespace Motor.Core.Modifiers.Visual;

public abstract class VisualModifierBase : ModifierBase
{
    [InjectModifier]
    protected Transform2dModifier _transform = null!;

    internal record VisualData : ModifierData
    {
        public Transform2dModifier.TransformData Transform;
    }

    internal VisualData PackInto(Serialization.ModifierPackingContext ctx, VisualData data)
    {
        data.Transform = (Transform2dModifier.TransformData)ctx.GetOrPack(_transform);
        return (PackInto(data) as VisualData)!;
    }

    public virtual void Draw()
    {
        throw new NotImplementedException();
    }
}