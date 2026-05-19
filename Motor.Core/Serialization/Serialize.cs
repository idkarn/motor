using Newtonsoft.Json;

namespace Motor.Core.Serialization;

static class Serializer
{
    static JsonSerializerSettings opts = new()
    {
        Formatting = Formatting.Indented,
        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            DefaultMembersSearchFlags =
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance,
        },
        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        ReferenceLoopHandling = ReferenceLoopHandling.Serialize
    };

    internal static void Serialize(Game game)
    {
        var json = JsonConvert.SerializeObject(game, opts);
        File.WriteAllText("game.json", json);
    }

    internal static Game? Deserialize(string fileName)
    {
        string json = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject<Game>(json, opts);
    }
}