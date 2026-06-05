using System.Numerics;

namespace Motor.Core.Modifiers;

[Guards.RegisterModifier("Transform2d", typeof(TransformData))]
public class Transform2dModifier : TransformModifierBase<Vector2, float, Matrix3x2>
{
    public static readonly Transform2dModifier Default = new();

    internal override void InitializeFromData(ModifierData data)
    {
        base.InitializeFromData(data);

        if (data is not TransformData transData) throw new Exception("not a Transform2d!");

        Position = transData.Position;
        Rotation = transData.Rotation;
        Scale = transData.Scale;
    }

    public Transform2dModifier()
    {
        Position = Vector2.Zero;
        WorldPosition = Vector2.Zero;

        Rotation = 0;
        WorldRotation = 0;

        Scale = Vector2.One;
        WorldScale = Vector2.One;

        WorldMatrix = new()
        {
            M11 = 1,
            M22 = 1
        };
    }

    public void Translate(Vector2 translate)
    {
        Position += translate;
    }

    public void Translate(float x, float y)
    {
        Position += new Vector2(x, y);
    }

    internal void UpdateWorldTransform(Transform2dModifier parentTransform)
    {
        // 1. Create individual 2D transformation states
        Matrix3x2 scaleMat = Matrix3x2.CreateScale(Scale);
        Matrix3x2 rotMat = Matrix3x2.CreateRotation(Rotation);
        Matrix3x2 transMat = Matrix3x2.CreateTranslation(Position);

        // 2. Combine into one local matrix (Order: Scale -> Rotate -> Translate)
        Matrix3x2 localMatrix = scaleMat * rotMat * transMat;

        // 3. Globalize: Multiply Local by Parent World Matrix
        WorldMatrix = localMatrix * parentTransform.WorldMatrix;

        // 4. Extract individual world values for easy access in your rendering/physics loops
        WorldPosition = WorldMatrix.Translation;

        // Mathematical extraction for global scale length and rotation angle from the matrix vectors
        WorldScale = new Vector2(
            new Vector2(WorldMatrix.M11, WorldMatrix.M12).Length(),
            new Vector2(WorldMatrix.M21, WorldMatrix.M22).Length()
        );
        WorldRotation = MathF.Atan2(WorldMatrix.M12, WorldMatrix.M11);
    }
}