using Motor.Core.Serialization;
using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

[Guards.RegisterModifier("Cirlce", typeof(CircleData))]
public class Circle : Shape
{
    public int Radius { get; set; } = 20;

    internal sealed record CircleData : ShapeData
    {
        public int Radius;
    }

    internal override CircleData PackToData(ModifierPackingContext ctx) => (PackInto(ctx, new CircleData()
    {
        Radius = Radius
    }) as CircleData)!;

    internal override void InitializeFromData(ModifierData data)
    {
        base.InitializeFromData(data);

        if (data is not CircleData circleData) throw new Exception("not a Circle!");

        Radius = circleData.Radius;
    }

    public override void Draw()
    {
        if (IsHollow)
            Raylib.DrawRing(_transform.WorldPosition, Radius - (LineWidth / 2.0f), Radius + (LineWidth / 2.0f), 0.0f, 360.0f, 0, _rayColor);
        else
            Raylib.DrawCircleV(_transform.WorldPosition, Radius, _rayColor);
    }
}