namespace Motor.Core.Serialization;

internal interface IPackable
{
    static abstract object PackToData(ModifierPackingContext ctx);

    static abstract object InstantiateFromData(object data);
}