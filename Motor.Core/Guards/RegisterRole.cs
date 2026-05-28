namespace Motor.Core.Guards;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class RegisterRoleAttribute(string roleName) : Attribute
{
    public string RoleName { get; } = roleName;
}