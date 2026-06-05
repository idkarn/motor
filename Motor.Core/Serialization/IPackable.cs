namespace Motor.Core.Serialization;

internal interface IPackable
{
    internal abstract object PackToData(ModifierPackingContext ctx);

    internal static abstract object InstantiateFromData(object data);
}