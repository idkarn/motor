using Motor.Core.Serialization;

namespace Motor.Core;

public class Game
{
    public required string Name;
    public Space MainScene = new();

    public void Save()
    {
        Serializer.Serialize(this);
    }

    public static Game LoadFromFile(string fileName)
    {
        var game = Serializer.Deserialize(fileName) ?? throw new Exception("Err: failed to load game from file!");
        return game;
    }
}