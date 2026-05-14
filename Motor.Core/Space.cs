using Motor.Core.Actors;
using Motor.Core.Modifiers;

namespace Motor.Core;

public class Space
{
    readonly List<Actor> _actors = [];

    internal void UpdateTransforms()
    {
        foreach (var actor in _actors)
            UpdateTransformTree(actor, Transform2dModifier.Default);
    }

    static void UpdateTransformTree(Actor actor, Transform2dModifier parentTransform)
    {
        var transform = actor.GetModifier<Transform2dModifier>()!;

        transform.UpdateWorldTransform(parentTransform);

        foreach (var child in actor.children)
            UpdateTransformTree(child, transform);
    }

    public void Add(Actor actor)
    {
        _actors.Add(actor);
    }
}
