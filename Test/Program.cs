using Motor.Core;
using Motor.Core.Actors;
using Motor.Core.Modifiers;
using Motor.Core.Modifiers.Controller;
using Motor.Core.Modifiers.Visual;

class PlayerController : Controller
{
    void Update(float dt)
    {
        Console.WriteLine($"upd {Transform.Scale}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var space = new Space();

        Player player = new();
        var playerTrans = player.GetModifier<Transform2dModifier>()!;
        playerTrans.Translate(20, 50);
        player.AddModifier(new Rectangle());
        player.AddModifier(new PlayerController());

        Dummy arm = new();
        var visMod = new Circle
        {
            Color = Color16.Red,
            Radius = 10
        };
        var armTrans = arm.GetModifier<Transform2dModifier>()!;
        armTrans.Translate(20, 20);
        arm.AddModifier(visMod);

        player.AddChild(arm);
        space.Add(player);

        Engine.Run(space);
    }
}
