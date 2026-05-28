using System.Numerics;
using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

[Guards.RegisterModifier("Rectangle", typeof(RectangleData))]
public class Rectangle : Shape
{
    public Vector2 Size { get; set; } = new(20, 20);

    internal sealed record RectangleData : ShapeData
    {
        public Vector2 Size;
    }

    internal override RectangleData PackToData(Serialization.ModifierPackingContext ctx) => (PackInto(ctx, new RectangleData()
    {
        Size = Size
    }) as RectangleData)!;

    internal static Rectangle InstantiateFromData(RectangleData data)
    {
        // todo: move other than Size to base class
        Rectangle instance = new()
        {
            Size = data.Size,
            Color = data.Color,
            IsHollow = data.IsHollow,
            LineWidth = data.LineWidth
        };

        return instance;
    }

    public override void Draw()
    {
        var rect = new Raylib_cs.Rectangle(_transform.WorldPosition - Size / 2, Size);

        if (IsHollow)
            Raylib.DrawRectangleLinesEx(rect, LineWidth, _rayColor);
        else
            Raylib.DrawRectangleRec(rect, _rayColor);
    }
}