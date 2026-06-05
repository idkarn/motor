using Motor.Core.Guards;
using Motor.Core.Modifiers.Area;
using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.UI;

public class Button<TArea, TVisual> : Interactive<TArea>
    where TArea : Area2d, new()
    where TVisual : VisualModifierBase, new()
{
    public string Text
    {
        get => GetModifier<Text>()!.Value;
        set => GetModifier<Text>()!.Value = value;
    }
    public Color16 TextColor
    {
        get => GetModifier<Text>()!.Color;
        set => GetModifier<Text>()!.Color = value;
    }

    public Button(bool isEmpty) : base(isEmpty)
    {
        if (!isEmpty)
        {
            AddModifier(new TVisual());
            AddModifier(new Text());
        }
    }
    public Button() : this(false) { }
}

[RegisterRole("RectButton")]
public class RectButton : Button<Modifiers.Area.Rectangle, Modifiers.Visual.Rectangle>
{
    public RectButton(bool isEmpty) : base(isEmpty) { }
    public RectButton() : base(false) { }
}

[RegisterRole("TextureButton")]
public class TextureButton : Button<Modifiers.Area.Rectangle, Texture>
{
    public TextureButton(bool isEmpty) : base(isEmpty) { }
    public TextureButton() : base(false) { }
}