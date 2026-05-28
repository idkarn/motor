using Motor.Core.Actors;
using Motor.Core.Modifiers;

namespace Motor.Core;

public class Space
{
    readonly List<Actor> _actors = [];

    internal sealed record SpaceData
    {
        public Actor.ActorData[] Actors = null!;
    }

    public Space(List<Actor> actors)
    {
        _actors = actors;
    }

    public Space()
    {
        _actors = [];
    }

    internal SpaceData PackToData(Serialization.ModifierPackingContext ctx) => new()
    {
        Actors = [.. _actors.Select(actor => actor.PackToData(ctx))]
    };

    internal static Space InstantiateFromData(SpaceData data) => new([.. data.Actors.Select(Actor.InstantiateFromData)]);

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
