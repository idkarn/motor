using System.Reflection;
using Motor.Core.Actors;

namespace Motor.Core.Modifiers.Controller;

class ControllerRef
{
    private readonly Action? _startMethod;
    private readonly Action<float>? _updateMethod;
    // private readonly Controller _instance;

    internal ControllerRef(Actor actor, IController instance)
    {
        instance.Actor = actor;
        instance.Input = Input.Input.Instance;

        // looking for Start()
        var startMethod = instance.GetType().GetMethod("Start", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (startMethod != null)
            _startMethod = (Action)Delegate.CreateDelegate(typeof(Action), instance, startMethod);

        // looking for Update() 
        var updateMethod = instance.GetType().GetMethod("Update", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (updateMethod != null)
            _updateMethod = (Action<float>)Delegate.CreateDelegate(typeof(Action<float>), instance, updateMethod);
    }

    internal void Start()
    {
        _startMethod?.Invoke();
    }

    internal void Update(float dt)
    {
        _updateMethod?.Invoke(dt);
    }
}