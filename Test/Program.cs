using Motor.Core;
using Motor.Core.Actors;
using Motor.Core.Modifiers;

class Player : Actor { }

class Arm : Actor { }

class Program
{
    static void Main(string[] args)
    {
        // Space space = new();
        Player player = new();
        Arm arm = new();
        // var transform = player.GetComponent<TransformModifier>();
        player.AddModifier(new RectangleVisualModifier());
        arm.AddModifier(new CircleVisualModifier());
        player.AddChild(arm);
        // space.Add(player);
        // space.Run();

        Engine.Run();
    }
}