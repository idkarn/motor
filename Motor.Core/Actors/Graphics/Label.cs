using Motor.Core.Guards;
using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

[RegisterRole("Label")]
public class Label : Actor
{
    public int FontSize
    {
        get => GetModifier<Text>()!.FontSize;
        set => GetModifier<Text>()!.FontSize = value;
    }
    public Color16 Color
    {
        get => GetModifier<Text>()!.Color;
        set => GetModifier<Text>()!.Color = value;
    }
    public string Text
    {
        get => GetModifier<Text>()!.Value;
        set => GetModifier<Text>()!.Value = value;
    }

    public Label(bool isEmpty) : base(isEmpty) { }
    public Label(string text) : base()
    {
        AddModifier(new Text()
        {
            Value = text,
        });
    }
}