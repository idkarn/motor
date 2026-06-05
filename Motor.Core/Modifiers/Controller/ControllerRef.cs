using System.Collections.Concurrent;
using System.Reflection;
using Motor.Core.Actors;
using Motor.Core.Guards;

namespace Motor.Core.Modifiers.Controller;

class ControllerRef
{
    static readonly ConcurrentDictionary<Type, ControllerHooks> _hooks = new();
    readonly Action? _startMethod;
    readonly Action<float>? _updateMethod;

    internal ControllerRef(Actor actor, IController instance)
    {
        instance.Actor = actor;
        instance.Input = Input.Input.Instance;
        ModifierDependencyInjector.InjectInto(instance, actor);

        var hooks = _hooks.GetOrAdd(instance.GetType(), static controllerType => BuildHooks(controllerType));
        _startMethod = hooks.Start is null ? null : (Action)Delegate.CreateDelegate(typeof(Action), instance, hooks.Start);
        _updateMethod = hooks.Update is null ? null : (Action<float>)Delegate.CreateDelegate(typeof(Action<float>), instance, hooks.Update);
    }

    internal void Start()
    {
        _startMethod?.Invoke();
    }

    internal void Update(float dt)
    {
        _updateMethod?.Invoke(dt);
    }

    static ControllerHooks BuildHooks(Type controllerType)
    {
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        var startMethod = controllerType.GetMethod("Start", flags, null, Type.EmptyTypes, null);
        var updateMethod = controllerType.GetMethod("Update", flags, null, [typeof(float)], null);

        return new ControllerHooks(startMethod, updateMethod);
    }

    readonly record struct ControllerHooks(MethodInfo? Start, MethodInfo? Update);
}