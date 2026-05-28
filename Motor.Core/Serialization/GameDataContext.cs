using System.Text.Json.Serialization;

namespace Motor.Core.Serialization;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    ReferenceHandler = JsonKnownReferenceHandler.Preserve,
    IncludeFields = true
)]
[JsonSerializable(typeof(Game.GameData))]
[JsonSerializable(typeof(Actors.Actor.ActorData))]
[JsonSerializable(typeof(Space.SpaceData))]
[JsonSerializable(typeof(Modifiers.ModifierBase.ModifierData))]
partial class GameDataContext : JsonSerializerContext { }