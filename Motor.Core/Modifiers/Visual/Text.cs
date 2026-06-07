using Motor.Core.Serialization;
using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

[Guards.RegisterModifier("Text", typeof(TextData))]
public class Text : VisualModifierBase
{
    public string Value { get; set; } = "";
    public int FontSize { get; set; } = 5;
    protected Color _rayColor;
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
    public bool IsCentered = false;

    internal sealed record TextData : VisualData
    {
        public string Value;
        public int FontSize;
        public Color16 Color;
        public bool IsCentered;
    }

    internal override ModifierData PackToData(ModifierPackingContext ctx) => (PackInto(ctx, new TextData
    {
        Value = Value,
        FontSize = FontSize,
        Color = _color,
        IsCentered = IsCentered
    }) as TextData)!;

    internal override void InitializeFromData(ModifierData data)
    {
        base.InitializeFromData(data);

        if (data is not TextData textData) throw new Exception("not a Text!");

        Value = textData.Value;
        FontSize = textData.FontSize;
        Color = textData.Color;
    }

    public override void Draw()
    {
        // todo: optimize text measuring on every frame!
        var origin = _transform.Position;
        if (IsCentered)
            origin -= Raylib.MeasureTextEx(Screen.Font, Value, FontSize, 1) / 2;
        Raylib.DrawTextEx(Screen.Font, Value, origin, FontSize, 1, _rayColor);
    }

    public Text()
    {
        Color = Color16.Green;
    }
}