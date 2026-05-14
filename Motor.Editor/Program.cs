using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;

namespace Motor.Editor;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        const int screenWidth = 800;
        const int screenHeight = 800;
        const int virtualWidth = 128; // The "pico" resolution
        const int virtualHeight = 128;
        const float scale = 6.25f;

        Raylib.InitWindow(screenWidth, screenHeight, "Test window");

        Raylib.SetTargetFPS(30);

        var target = Raylib.LoadRenderTexture(virtualWidth, virtualHeight);
        Raylib.SetTextureFilter(target.Texture, TextureFilter.Point);

        rlImGui.Setup();

        // Loop until the window is closed
        while (!Raylib.WindowShouldClose())
        {
            var mousePos = Raylib.GetMousePosition();
            var virtualMouse = new Vector2(mousePos.X / scale, mousePos.Y / scale);

            // --- RENDERING INTO TEXTURE ---
            Raylib.BeginTextureMode(target);
            {
                Raylib.ClearBackground(Color.Blank);

                rlImGui.Begin();

                var io = ImGui.GetIO();
                io.MousePos = virtualMouse;
                io.DisplaySize = new Vector2(virtualWidth, virtualHeight);

                ImGui.Begin("Test");
                ImGui.Text($"{io.MousePos.X}, {io.MousePos.Y}");
                ImGui.End();

                Console.WriteLine($"{io.MousePos} {io.DisplaySize}");

                rlImGui.End();
            }
            Raylib.EndTextureMode();

            // --- RENDERING ON SCREEN ---
            Raylib.BeginDrawing();
            {
                Raylib.ClearBackground(Color.Black);

                Rectangle source = new(0, 0, virtualWidth, -virtualHeight);
                Rectangle dest = new(0, 0, virtualWidth, virtualHeight);

                Raylib.DrawTexturePro(target.Texture, source, dest, Vector2.Zero, 0, Color.White);

                Raylib.DrawFPS(10, 10);
            }
            Raylib.EndDrawing();
        }

        rlImGui.Shutdown();

        // Close the window
        Raylib.CloseWindow();
    }
}