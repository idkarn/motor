using Motor.Core.Actors;
using Motor.Core.Guards;
using Motor.Core.Modifiers;
using Motor.Core.Modifiers.Controller;
using Motor.Core.Modifiers.Visual;

namespace Motor.Core;

static class ModifiersRegistry
{
    readonly static List<VisualModifierBase> _drawables = [];
    readonly static List<ControllerRef> _controllers = [];

    internal static void AddModifier(Actor actor, ModifierBase modifier)
    {
        if (modifier is VisualModifierBase _drawableMod)
            _drawables.Add(_drawableMod);
        else if (modifier is IController _controllerMod)
            _controllers.Add(new ControllerRef(actor, _controllerMod));
        else if (modifier is ControllerScript script)
            _controllers.Add(new ControllerRef(actor, script.InstantiateController()));

        InjectDependencies(actor, modifier);
    }

    internal static void StartAll()
    {
        foreach (var controller in _controllers)
            controller.Start();
    }

    internal static void UpdateAll(float dt)
    {
        foreach (var controller in _controllers)
            controller.Update(dt);
    }

    internal static void DrawAll()
    {
        foreach (var drawable in _drawables)
            if (drawable.IsEnabled)
                drawable.Draw();
    }

    static void InjectDependencies(Actor actor, ModifierBase modifier)
    {
        ModifierDependencyInjector.InjectInto(modifier, actor);
    }
}