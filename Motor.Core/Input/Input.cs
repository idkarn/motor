using System.Numerics;
using Raylib_cs;

namespace Motor.Core.Input;

public class Input
{
    static Input _instance = new();
    internal static Input Instance => _instance;
    public static event Action<KeyboardKey>? KeyPressed;
    public static event Action? OnHover;

    public static bool IsKeyDown(KeyboardKey button) => InputManager.IsKeyDown(button);
    public static bool IsKeyPressed(KeyboardKey button) => InputManager.IsKeyPressed(button);
    public static bool IsMouseDown(MouseButton button) => InputManager.IsMouseDown(button);
    public static bool IsMousePressed(MouseButton button) => InputManager.IsMousePressed(button);
    public static bool IsMouseOver(Rectangle area) => InputManager.IsMouseOver(area);
    public static bool IsMouseOver(Vector2 center, float radius) => InputManager.IsMouseOver(center, radius);
    public static Vector2 GetMousePosition() => InputManager.GetMousePosition();

    Input()
    {
        InputManager.OnKeyPressed += OnKeyPressed;
    }

    internal void OnKeyPressed(KeyboardKey key)
    {
        KeyPressed?.Invoke(key);
    }
}