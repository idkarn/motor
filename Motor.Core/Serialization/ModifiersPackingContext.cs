using Motor.Core.Modifiers;

namespace Motor.Core.Serialization;

class ModifierPackingContext
{
    readonly Dictionary<ModifierBase, ModifierBase.ModifierData> _packedCache = [];

    internal ModifierBase.ModifierData GetOrPack(ModifierBase mod)
    {
        if (_packedCache.TryGetValue(mod, out var cachedData))
            return cachedData;

        var newData = mod.PackToData(this);
        _packedCache[mod] = newData;
        return newData;
    }
}