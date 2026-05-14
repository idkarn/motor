using Motor.Core.Actors;

namespace Motor.Core;

public class Space
{
    List<Actor> actors = [];
    public void Run()
    {
    }

    public void Add(Actor actor)
    {
        actors.Add(actor);
    }
}
