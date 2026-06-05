using Motor.Core.Actors;
using Motor.Core.Guards;

namespace Motor.Core.Modifiers.Controller;

public interface IController
{
    Input.Input Input { get; set; }
    Actor Actor { get; set; }
    Transform2dModifier Transform { get; set; }
}

public abstract class Controller<TActor> : ModifierBase, IController
    where TActor : Actor
{
    // [InjectModifier] // check ControllerRef for solution
    public Transform2dModifier Transform { get; set; } = null!;
    public TActor Actor { get; private set; } = null!;
    Actor IController.Actor
    {
        get => this.Actor;
        set => this.Actor = (TActor)value;
    }
    public Input.Input Input { get; set; } = null!;
    internal string Name = null!;
    public TModifier? GetModifier<TModifier>() where TModifier : ModifierBase
        => Actor.GetModifier<TModifier>();
    public ModifierBase? GetModifier(Type type) => Actor.GetModifier(type);
}

public abstract class Controller : Controller<Actor> { }