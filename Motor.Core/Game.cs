using Motor.Core.Serialization;

namespace Motor.Core;

public class Game
{
    public required string Name;
    public Space MainScene = new();

    internal sealed record GameData
    {
        public string Name = null!;
        public Space.SpaceData MainSpace = null!;
    }

    internal GameData PackToData() => new()
    {
        Name = Name,
        MainSpace = MainScene.PackToData(new ModifierPackingContext())
    };

    internal static Game InstantiateFromData(GameData data)
    {
        return new()
        {
            Name = data.Name,
            MainScene = Space.InstantiateFromData(data.MainSpace)
        };
    }

    public void Save()
    {
        Serializer.Serialize(PackToData());
    }

    public static Game LoadFromFile(string fileName)
    {
        var data = Serializer.Deserialize(fileName) ?? throw new Exception("Err: failed to load game from file!");

        return InstantiateFromData(data);
    }
}