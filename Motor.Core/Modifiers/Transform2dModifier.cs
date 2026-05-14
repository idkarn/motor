using System.Numerics;

namespace Motor.Core.Modifiers;

public class Transform2dModifier : ModifierBase
{
    public Vector2 Position { get; set; } = new(0, 0);
    public float Rotation { get; set; } = 0;
    public Vector2 Scale { get; set; } = new(1, 1);

    public static readonly Transform2dModifier Initial = new();
}