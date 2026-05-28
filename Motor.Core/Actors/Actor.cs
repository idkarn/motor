using System.Numerics;
using System.Reflection;
using Motor.Core.Modifiers;

namespace Motor.Core.Actors;

public abstract class Actor
{
    readonly Dictionary<Type, List<ModifierBase>> _modifiers = [];
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

    internal record ActorData
    {
        public string Role = null!;
        public ModifierBase.ModifierData[] Modifiers = null!;
        public List<Actor> Children = null!;
    }

    protected Actor(bool isEmpty = false)
    {
        if (!isEmpty)
            AddModifier(new Transform2dModifier());
    }

    protected Actor() : this(false) { }

    internal virtual ActorData PackToData(Serialization.ModifierPackingContext ctx) => new()
    {
        Role = GetType().GetCustomAttribute<Guards.RegisterRoleAttribute>()?.RoleName ?? "unknown",
        Modifiers = [.. _modifiers.Values.SelectMany(list => list).Distinct().Select(ctx.GetOrPack)],
        Children = children
    };

    internal static Actor InstantiateFromData(ActorData data)
    {
        var instance = ActorActivator.Create(data.Role);

        foreach (var mod in data.Modifiers)
            instance.AddModifier(ModifierBase.InstantiateFromData(mod));

        return instance;
    }

    public void AddChild(Actor child)
    {
        children.Add(child);
    }

    public void AddModifier(ModifierBase modifier)
    {
        Type type = modifier.GetType();

        AddModifierMapping(type, modifier);

        // 2. Map by all interfaces it implements (for interface lookups!)
        foreach (Type interfaceType in type.GetInterfaces())
        {
            AddModifierMapping(interfaceType, modifier);
        }

        // 3. Map by base classes (if navigating an inheritance tree)
        Type? baseType = type.BaseType;
        while (baseType != typeof(ModifierBase) && baseType != null)
        {
            AddModifierMapping(baseType, modifier);
            baseType = baseType.BaseType;
        }

        ModifiersRegistry.AddModifier(this, modifier);
    }

    void AddModifierMapping(Type type, ModifierBase modifier)
    {
        if (!_modifiers.TryGetValue(type, out var list))
        {
            list = [];
            _modifiers[type] = list;
        }
        if (!list.Contains(modifier))
            list.Add(modifier);
    }

    public T? GetModifier<T>() where T : ModifierBase => GetModifier(typeof(T)) as T;

    public ModifierBase? GetModifier(Type type)
    {
        if (_modifiers.TryGetValue(type, out var list) && list.Count > 0)
            return list[0];
        return null;
    }
}

public static partial class ActorActivator
{
    public static partial Actor Create(string role);
}