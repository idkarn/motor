using System.Numerics;
using Motor.Core.Modifiers;

namespace Motor.Core.Actors;

public abstract class Actor
{
    readonly Dictionary<Type, ModifierBase> _modifiers = [];
    readonly internal List<Actor> children = [];
    public Vector2 Position
    {
        get => GetModifier<Transform2dModifier>()!.Position;
        set => GetModifier<Transform2dModifier>()!.Position = value;
    }
    public Vector2 Scale
    {
        get => GetModifier<Transform2dModifier>()!.Scale;
        set => GetModifier<Transform2dModifier>()!.Scale = value;
    }

    protected Actor()
    {
        AddModifier(new Transform2dModifier());
    }

    public void AddChild(Actor child)
    {
        children.Add(child);
    }

    public void AddModifier(ModifierBase modifier)
    {
        Type type = modifier.GetType();

        _modifiers[type] = modifier;

        // 2. Map by all interfaces it implements (for interface lookups!)
        foreach (Type interfaceType in type.GetInterfaces())
        {
            _modifiers[interfaceType] = modifier;
        }

        // 3. Map by base classes (if navigating an inheritance tree)
        Type? baseType = type.BaseType;
        while (baseType != typeof(ModifierBase) && baseType != null)
        {
            _modifiers[baseType] = modifier;
            baseType = baseType.BaseType;
        }

        ModifiersRegistry.AddModifier(this, modifier);
    }

    public T? GetModifier<T>() where T : ModifierBase
    {
        if (_modifiers.TryGetValue(typeof(T), out var mod))
            return (T)mod;
        return null;
    }

    public ModifierBase? GetModifier(Type type)
    {
        if (_modifiers.TryGetValue(type, out var mod))
            return mod;
        return null;
    }
}