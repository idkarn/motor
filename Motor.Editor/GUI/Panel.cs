using System.Numerics;
using Motor.Core.Actors;
using Motor.Core.Actors.Graphics;
using Motor.Editor.Controllers;

namespace Motor.Editor.GUI;

class Panel : Actor
{
    RectangleShape _shape = new()
    {
        IsHollow = true,
        Color = Core.Color16.DarkGrey
    };
    public Vector2 Size
    {
        get => _shape.Size;
        set => _shape.Size = value;
    }

    public Panel(Vector2 position, Vector2 size)
    {
        AddChild(_shape);

        Position = position;
        Size = size;

        AddModifier(new PanelController());
    }
}