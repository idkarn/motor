using System.Numerics;
using Motor.Core.Serialization;

namespace Motor.Core.Modifiers.Area;

[Guards.RegisterModifier("RectangleArea", typeof(RectangleAreaData))]
public class Rectangle : Area2d
{
    public Vector2 Size { get; set; } = new(20, 20);

    internal sealed record RectangleAreaData : AreaData
    {
        public Vector2 Size;
    }

    internal override RectangleAreaData PackToData(ModifierPackingContext ctx) => (PackInto(ctx, new RectangleAreaData()
    {
        Size = Size
    }) as RectangleAreaData)!;

    internal override void InitializeFromData(ModifierData data)
    {
        base.InitializeFromData(data);

        if (data is not RectangleAreaData rectData) throw new Exception("not a RectangleArea!");

        Size = rectData.Size;
    }

    public override bool IsMouseOver()
    {
        return Input.InputManager.IsMouseOver(new Raylib_cs.Rectangle(_transform.WorldPosition - Size / 2, Size));
    }
}