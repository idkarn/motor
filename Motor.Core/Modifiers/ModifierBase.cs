using System.Reflection;
using System.Text.Json.Serialization;

namespace Motor.Core.Modifiers;

public abstract class ModifierBase
{
    public bool IsEnabled { get; set; } = true;

    [JsonPolymorphic]
    [JsonDerivedType(typeof(Transform2dModifier.TransformData), "transform2d")]
    [JsonDerivedType(typeof(Visual.Rectangle.RectangleData), "rectangle")]
    [JsonDerivedType(typeof(Visual.Circle.CircleData), "circle")]
    [JsonDerivedType(typeof(Visual.Text.TextData), "text")]
    [JsonDerivedType(typeof(Visual.Texture.TextureData), "texture")]
    [JsonDerivedType(typeof(Area.Rectangle.RectangleAreaData), "rectangleArea")]
    [JsonDerivedType(typeof(Area.CircleArea.CircleAreaData), "circleArea")]
    [JsonDerivedType(typeof(Controller.ControllerScript.ScriptData), "script")]
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

    internal virtual void InitializeFromData(ModifierData data)
    {
        IsEnabled = data.IsEnabled;
    }

    internal static ModifierBase InstantiateFromData(ModifierData data)
    {
        var instance = ModifierActivator.Create(data.Name);

        instance.InitializeFromData(data);

        return instance;
    }
}

internal static partial class ModifierActivator
{
    public static partial ModifierBase Create(string name);
}