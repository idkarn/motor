using Motor.Core.Guards;

namespace Motor.Core.Modifiers.Controller;

public abstract class Controller : ModifierBase
{
    [InjectModifier]
    public Transform2dModifier Transform = null!;
    internal string Name = null!;
    // required public PhysicsModifier Physics;
    // required public Input Input;
    // public virtual void OnCollisionEnter(Actor other) { }
}