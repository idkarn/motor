using System.Reflection;
using System.Text.Json.Serialization;

namespace Motor.Core.Modifiers;

public abstract class ModifierBase
{
    public bool IsEnabled { get; set; } = true;

    [JsonPolymorphic]
    // [JsonDerivedType(typeof(Transform2dModifier.TransformData), "transform2d")]
    // [JsonDerivedType(typeof(Visual.Rectangle.RectangleData), "rectangle")]
    internal partial record ModifierData
    {
        public string Name = null!;
        public bool IsEnabled;
    }

    // todo: make abstract
    internal virtual ModifierData PackToData(Serialization.ModifierPackingContext ctx) { return new ModifierData(); }

    internal ModifierData PackInto(ModifierData data)
    {
        data.Name = GetType().GetCustomAttribute<Guards.RegisterModifierAttribute>()?.ModifierName ?? "unknown";
        data.IsEnabled = IsEnabled;
        return data;
    }

    internal static ModifierBase InstantiateFromData(ModifierData data)
    {
        var instance = ModifierActivator.Create(data.Name, data);

        instance.IsEnabled = data.IsEnabled;

        return instance;
    }
}

internal static partial class ModifierActivator
{
    public static partial ModifierBase Create(string name, ModifierBase.ModifierData data);
}