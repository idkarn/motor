using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

public abstract class Shape : VisualModifierBase
{
    protected Color _rayColor;
    public bool IsHollow { get; set; } = false;
    public int LineWidth { get; set; } = 1;
    Color16 _color;
    public Color16 Color
    {
        get => _color;
        set
        {
            _color = value;
            var clr = value.ToColor();
            _rayColor = new Color(clr.R, clr.G, clr.B, clr.A);
        }
    }

    internal record ShapeData : VisualData
    {
        public bool IsHollow;
        public int LineWidth;
        public Color16 Color;
    }

    internal ShapeData PackInto(Serialization.ModifierPackingContext ctx, ShapeData data)
    {
        data.IsHollow = IsHollow;
        data.LineWidth = LineWidth;
        data.Color = _color;
        return (base.PackInto(ctx, data) as ShapeData)!;
    }

    internal override void InitializeFromData(ModifierData data)
    {
        base.InitializeFromData(data);

        if (data is not ShapeData shapeData) throw new Exception("not a Shape!");

        IsHollow = shapeData.IsHollow;
        LineWidth = shapeData.LineWidth;
        Color = shapeData.Color;
    }

    public Shape()
    {
        Color = Color16.Green;
    }
}