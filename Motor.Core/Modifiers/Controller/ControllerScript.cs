using Motor.Core.Serialization;

namespace Motor.Core.Modifiers.Controller;

[Guards.RegisterModifier("Script", typeof(ScriptData))]
public class ControllerScript : ModifierBase
{
    public string ClassName = null!;

    internal sealed record ScriptData : ModifierData
    {
        public string ClassName;
    }

    internal override ModifierData PackToData(ModifierPackingContext ctx) => (PackInto(new ScriptData()
    {
        ClassName = ClassName
    }) as ScriptData)!;

    internal override void InitializeFromData(ModifierData data)
    {
        base.InitializeFromData(data);

        if (data is not ScriptData scriptData) throw new Exception("not a Script!");

        ClassName = scriptData.ClassName;
    }

    internal IController InstantiateController()
    {
        return ScriptManager.GetScriptByName(ClassName);
    }
}