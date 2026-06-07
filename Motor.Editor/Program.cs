using System.Numerics;
using Motor.Core;
using Motor.Core.Actors.Graphics;
using Motor.Core.Actors.UI;
using Motor.Core.Modifiers.Controller;
using Motor.Core.Modifiers.Visual;
using Motor.Editor.GUI;

namespace Motor.Editor;

class CreatorController(Action createNew) : Controller<RectButton>
{
    Action _createNew = createNew;
    void Start()
    {
        GetModifier<Core.Modifiers.Area.Area2d>()!.IgnoreMouse = false;
        Actor.Click += OnClick;
    }

    void OnClick()
    {
        _createNew?.Invoke();
    }
}

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var game = new Game()
        {
            Name = "Editor"
        };

        var outliner = new Outliner(
            new Vector2(25, Screen.Height * 0.66f / 2 + 9),
            new Vector2(50, Screen.Height * 0.66f)
        );

        var viewer = new View(
            new Vector2(89, Screen.Height * 0.66f / 2 + 9),
            new Vector2(Screen.Width - 50, Screen.Height * 0.66f)
        );

        var toolbar = new RectangleShape
        {
            Position = new Vector2(Screen.Width / 2, 4),
            Size = new Vector2(Screen.Width, 9),
            IsHollow = true,
            Color = Color16.DarkGrey
        };

        var border = new RectangleShape
        {
            Position = new Vector2(Screen.Width / 2, Screen.Height / 2),
            Size = new Vector2(Screen.Width, Screen.Height),
            IsHollow = true,
        };

        var title = new Label("Motor")
        {
            Color = Color16.DarkGreen,
            Position = new Vector2(2, 2),
        };

        Engine.Init();

        var circle = new CircleShape()
        {
            Radius = 10,
            Position = new Vector2(10, 10),
            Name = "lil circle",
        };
        circle.AddTag("User");

        var creatorBtn = new RectButton()
        {
            Text = "add actor",
            Position = new Vector2(100, 4),
            TextColor = Color16.Black,
        };
        creatorBtn.GetModifier<Rectangle>()!.Size = new Vector2(40, 7);
        creatorBtn.GetModifier<Text>()!.IsCentered = true;
        creatorBtn.AddModifier(new CreatorController(() =>
        {
            var newActor = new RectangleShape()
            {
                Color = Color16.Red,
                Position = new Vector2(10, 10),
                Name = "square"
            };
            outliner.AddActor(newActor);
            viewer.AddActor(newActor);
        }));

        game.MainScene.Add(outliner);
        game.MainScene.Add(viewer);
        game.MainScene.Add(toolbar);
        game.MainScene.Add(border);
        game.MainScene.Add(title);
        game.MainScene.Add(creatorBtn);

        outliner.AddActor(circle);
        viewer.AddActor(circle);

        // game.Save();

        // var game = Game.LoadFromFile("game.json");

        Engine.Load(game);
        Engine.Start();
    }
}