using System.Numerics;
using Motor.Core.Actors;
using Motor.Core.Actors.Graphics;

namespace Motor.Editor.GUI;

class Outliner(Vector2 position, Vector2 size) : Panel(position, size)
{
    List<Actor> _actors = [];
    List<Label> _labels = [];

    public void AddActor(Actor actor)
    {
        _actors.Add(actor);
        _labels.Add(new Label(actor.Name)
        {
            Position = new Vector2(3, 10 * (_actors.Count - 1) + 12),
            FontSize = 5
        });
    }
}