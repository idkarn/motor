namespace Motor.Core.Modifiers;

public abstract class TransformModifierBase<TVector, TRotation, TMatrix> : ModifierBase
    where TVector : struct
    where TMatrix : struct
{
    public TVector Position { get; set; }
    public TRotation Rotation { get; set; }
    public TVector Scale { get; set; }
    public TVector WorldPosition { get; protected set; }
    public TRotation WorldRotation { get; protected set; }
    public TVector WorldScale { get; protected set; }
    protected TMatrix WorldMatrix;
}