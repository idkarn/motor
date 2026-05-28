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

    internal sealed record TransformData : ModifierData
    {
        public TVector Position;
        public TRotation Rotation;
        public TVector Scale;
    }

    internal override TransformData PackToData(Serialization.ModifierPackingContext ctx) => (PackInto(new TransformData()
    {
        Position = Position,
        Rotation = Rotation,
        Scale = Scale
    }) as TransformData)!;

    internal static TransformModifierBase<TVector, TRotation, TMatrix> InstantiateFromData(TransformData data)
    {
        throw new Exception("not implemented!");
    }
}