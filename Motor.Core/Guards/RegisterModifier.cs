namespace Motor.Core.Guards;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class RegisterModifierAttribute(string modifierName, Type dtoType) : Attribute
{
    public string ModifierName { get; } = modifierName;
    public Type DtoType { get; } = dtoType;
}