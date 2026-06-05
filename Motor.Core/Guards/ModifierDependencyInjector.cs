using System.Reflection;
using Motor.Core.Actors;

namespace Motor.Core.Guards;

internal static class ModifierDependencyInjector
{
    const BindingFlags MemberFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

    internal static void InjectInto(object target, Actor actor)
    {
        var targetType = target.GetType();

        for (var current = targetType; current is not null && current != typeof(object); current = current.BaseType)
        {
            foreach (var field in current.GetFields(MemberFlags | BindingFlags.DeclaredOnly))
                TryInjectMember(field.FieldType, value => field.SetValue(target, value), field.IsDefined(typeof(InjectModifierAttribute), false), actor, field.Name, current.Name);

            foreach (var property in current.GetProperties(MemberFlags | BindingFlags.DeclaredOnly))
            {
                if (!property.CanWrite || property.GetIndexParameters().Length > 0)
                    continue;

                TryInjectMember(property.PropertyType, value => property.SetValue(target, value), property.IsDefined(typeof(InjectModifierAttribute), false), actor, property.Name, current.Name);
            }
        }
    }

    static void TryInjectMember(Type requiredType, Action<object?> assign, bool isInjectMarked, Actor actor, string memberName, string ownerName)
    {
        if (!isInjectMarked)
            return;

        var dependency = actor.GetModifier(requiredType);
        if (dependency != null)
        {
            assign(dependency);
            return;
        }

        Console.WriteLine($"Entity {actor} is missing a {requiredType.Name} required by {ownerName}.{memberName}!");
    }
}