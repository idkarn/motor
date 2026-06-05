using System.Text.Json;

namespace Motor.Core.Serialization;

static class Serializer
{
    internal static void Serialize(Game.GameData data)
    {
        var json = JsonSerializer.Serialize(data, GameDataContext.Default.GameData);
        File.WriteAllText("game.json", json);
    }

    internal static Game.GameData? Deserialize(string filename)
    {
        string json = File.ReadAllText(filename);
        return JsonSerializer.Deserialize(json, GameDataContext.Default.GameData);
    }
}