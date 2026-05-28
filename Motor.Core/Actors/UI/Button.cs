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

    public Button()
    {
        _visual = new TVisual();
        AddModifier(_visual);

        _text = new Text();
        AddModifier(_text);
    }
}

public class Button : Button<Modifiers.Area.Rectangle, Modifiers.Visual.Rectangle> { }