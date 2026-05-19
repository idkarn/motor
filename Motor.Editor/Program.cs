using System.Numerics;
using Motor.Core;
using Motor.Core.Actors.Graphics;
using Motor.Core.Actors.UI;
using Motor.Core.Input;
using Motor.Core.Modifiers.Controller;
using Motor.Core.Modifiers.Visual;

namespace Motor.Editor;

class ButtonController : Controller<Button<Core.Modifiers.Area.Rectangle, Texture>>
{
    bool isMouseOver = false;
    bool dragged = false;
    Vector2 shift;

    void Start()
    {
        GetModifier<Core.Modifiers.Area.Area2d>()!.IgnoreMouse = false;
        Actor.MouseEnter += () => isMouseOver = true;
        Actor.MouseExit += () => isMouseOver = false;
    }

    void Update(float dt)
    {
        // if (isMouseOver || dragged)
        //     GetModifier<Texture>()!.Color = Color16.Red;
        // else
        //     GetModifier<Texture>()!.Color = Color16.Green;

        if (!dragged && isMouseOver && Input.IsMouseDown(MouseButton.Left))
        {
            dragged = true;
            // Actor.Text = "uwu";
            shift = Transform.Position - Input.GetMousePosition();
            // GetModifier<Texture>()!.Color = Color16.Red;
        }
        else if (dragged && !Input.IsMouseDown(MouseButton.Left))
        {
            dragged = false;
            // Actor.Text = "drag me";
            // GetModifier<Texture>()!.Color = Color16.Green;
        }
        if (dragged)
            Transform.Position = Input.GetMousePosition() + shift;
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

        var tree = new RectangleShape
        {
            Position = new Vector2(25, Screen.Height * 0.66f / 2 + 8),
            Size = new Vector2(50, Screen.Height * 0.66f),
            IsHollow = true,
            Color = Color16.DarkGrey
        };

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
            Position = new Vector2(30, 5)
        };

        Engine.Init();

        var btn = new Button<Core.Modifiers.Area.Rectangle, Texture>
        {
            Position = new Vector2(Screen.Width / 2, Screen.Height / 2),
            // Text = "drag me",
            TextColor = Color16.White,
            Scale = new Vector2(0.3f, 0.3f)
        };
        btn.AddModifier(new ButtonController());
        btn.GetModifier<Texture>()!.Load(Path.Combine(Path.Combine(AppContext.BaseDirectory, "data"), "X.png"));

        game.MainScene.Add(tree);
        game.MainScene.Add(border);
        game.MainScene.Add(toolbar);
        game.MainScene.Add(btn);

        // game.Save();

        Engine.Load(game);
        Engine.Start();
    }
}