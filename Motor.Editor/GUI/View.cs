using System.Numerics;
using Motor.Core.Actors;
using Motor.Core.Actors.UI;
using Motor.Core.Modifiers.Area;
using Motor.Editor.Controllers;

namespace Motor.Editor.GUI;

class View(Vector2 position, Vector2 size) : Panel(position, size)
{
    List<Actor> _actors = [];

    public void AddActor(Actor actor)
    {
        _actors.Add(actor);
        var container = new RectArea()
        {
            Position = actor.Position
        };
        actor.Position = Vector2.Zero;
        container.AddModifier(new DraggableRect());
        container.AddChild(actor);
        AddChild(container);
    }
}