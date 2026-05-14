using System.Numerics;
using Raylib_cs;

namespace Motor.Core;

public static class Screen
{
    const int _screenWidth = 800;
    const int _screenHeight = 800;
    const string _windowTitle = "Motor Test";
    public const int Height = 128;
    public const int Width = 128;
    static RenderTexture2D target;
    internal static bool IsOpen => !Raylib.WindowShouldClose();

    internal static void Init()
    {
        Raylib.InitWindow(_screenWidth, _screenHeight, _windowTitle);

        Raylib.SetTargetFPS(30);

        target = Raylib.LoadRenderTexture(Width, Height);

        Raylib.SetTextureFilter(target.Texture, TextureFilter.Point);
    }

    internal static void Begin()
    {
        Raylib.BeginTextureMode(target);
        Raylib.ClearBackground(new Color(20, 20, 25, 255));
    }

    internal static void End()
    {
        Raylib.EndTextureMode();


        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        Rectangle source = new(0, 0, Width, -Height);
        Rectangle dest = new(0, 0, _screenWidth, _screenHeight);

        Raylib.DrawTexturePro(target.Texture, source, dest, Vector2.Zero, 0, Color.White);

        Raylib.DrawFPS(10, 10);
        Raylib.EndDrawing();
    }

    internal static void Close()
    {
        Raylib.UnloadRenderTexture(target);
        Raylib.CloseWindow();
    }
}