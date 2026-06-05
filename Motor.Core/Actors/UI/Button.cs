using Motor.Core.Guards;
using Motor.Core.Modifiers.Area;
using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.UI;

public class Button<TArea, TVisual> : Interactive<TArea>
    where TArea : Area2d, new()
    where TVisual : VisualModifierBase, new()
{
    readonly TVisual _visual;
    readonly Text _text;
    public string Text
    {
        get => _text.Value;
        set => _text.Value = value;
    }
    public Color16 TextColor
    {
        get => _text.Color;
        set => _text.Color = value;
    }

    public Button(bool isEmpty) : base(isEmpty)
    {
        _visual = new TVisual();
        _text = new Text();

        if (!isEmpty)
        {
            AddModifier(_visual);
            AddModifier(_text);
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