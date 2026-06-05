using System.Text.Json.Serialization;

namespace Motor.Core.Modifiers;

public abstract partial class ModifierBase
{
    [JsonDerivedType(typeof(Transform2dModifier.TransformData), "transform2d")]
    [JsonDerivedType(typeof(Visual.Rectangle.RectangleData), "rectangle")]
    [JsonDerivedType(typeof(Visual.Circle.CircleData), "circle")]
    [JsonDerivedType(typeof(Visual.Text.TextData), "text")]
    [JsonDerivedType(typeof(Visual.Texture.TextureData), "texture")]
    [JsonDerivedType(typeof(Area.Rectangle.RectangleAreaData), "rectangleArea")]
    [JsonDerivedType(typeof(Area.CircleArea.CircleAreaData), "circleArea")]
    [JsonDerivedType(typeof(Controller.ControllerScript.ScriptData), "script")]
    internal partial record ModifierData { }
}