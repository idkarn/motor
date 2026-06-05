using Motor.Core.Serialization;

namespace Motor.Core.Modifiers.Area;


// todo: replace this thing with common Circle
[Guards.RegisterModifier("CircleArea", typeof(CircleAreaData))]
public class CircleArea : Area2d
{
    public int Radius { get; set; } = 20;

    internal sealed record CircleAreaData : AreaData
    {
        public int Radius;
    }

    internal override ModifierData PackToData(ModifierPackingContext ctx) => (PackInto(ctx, new CircleAreaData()
    {
        Radius = Radius
    }) as CircleAreaData)!;

    internal override void InitializeFromData(ModifierData data)
    {
        base.InitializeFromData(data);

        if (data is not CircleAreaData rectData) throw new Exception("not a CircleArea!");

        Radius = rectData.Radius;
    }

    public override bool IsMouseOver()
    {
        return Input.InputManager.IsMouseOver(_transform.WorldPosition, Radius);
    }
}