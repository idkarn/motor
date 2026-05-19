using System.Numerics;
using Motor.Core;
using Motor.Core.Actors;
using Motor.Core.Actors.Graphics;
using Motor.Core.Modifiers;
using Motor.Core.Modifiers.Controller;
using Motor.Core.Modifiers.Visual;

class PlayerController : Controller
{
    void Start()
    {
        Input.KeyPressed += OnKeyPressed;
    }

    void OnKeyPressed(KeyboardKey key)
    {
        Console.WriteLine($"{key}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var game = new Game()
        {
            Name = "Example"
        };

        Player player = new();
        var playerTrans = player.GetModifier<Transform2dModifier>()!;
        playerTrans.Translate(20, 50);
        player.AddModifier(new Rectangle() { IsHollow = true });
        player.AddModifier(new PlayerController());

        Dummy arm = new();
        var visMod = new Circle
        {
            Color = Color16.Red,
            Radius = 10,
            IsHollow = true
        };
        var armTrans = arm.GetModifier<Transform2dModifier>()!;
        armTrans.Translate(20, 20);
        arm.AddModifier(visMod);

        player.AddChild(arm);
        game.MainScene.Add(player);

        Label label = new("hello, world!");
        label.GetModifier<Transform2dModifier>()!.Position = new Vector2(100, 100);
        game.MainScene.Add(label);

        Engine.Init();
        Engine.Load(game);
        Engine.Start();
    }
}
