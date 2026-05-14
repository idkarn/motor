using System.Reflection;
using Motor.Core.Actors;
using Motor.Core.Guards;
using Motor.Core.Modifiers;

namespace Motor.Core;

static class ModifiersRegistry
{
    readonly static List<VisualModifierBase> _drawables = [];

    internal static void AddActor() { }
    internal static void AddModifier(Actor actor, ModifierBase modifier)
    {
        if (modifier is VisualModifierBase _drawableMod)
            _drawables.Add(_drawableMod);

        InjectDependencies(actor, modifier);
    }

    internal static void UpdateAll()
    {

    }

    internal static void DrawAll()
    {
        foreach (var drawable in _drawables)
            if (drawable.IsEnabled)
                drawable.Draw();
    }

    static void InjectDependencies(Actor actor, ModifierBase modifier)
    {
        // Get all fields of the component class (including private fields)
        FieldInfo[] fields = modifier.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (FieldInfo field in fields)
        {
            // Check if the field is marked with our [InjectComponent] attribute
            if (field.IsDefined(typeof(InjectModifierAttribute), false))
            {
                Type requiredType = field.FieldType;

                var dependency = actor.GetModifier(requiredType);
                if (dependency != null)
                {
                    field.SetValue(modifier, dependency);
                }
                else
                {
                    Console.WriteLine($"Entity {actor} is missing a {requiredType.Name} required by {modifier.GetType().Name}!");
                }
            }
        }
    }
}