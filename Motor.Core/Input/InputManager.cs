using System.Numerics;
using Raylib_cs;

namespace Motor.Core.Input;

static class InputManager
{
    static readonly MouseBuffer _mouseFrameState = new();
    internal static Span<ButtonState> MouseFrameState => _mouseFrameState.State;
    static readonly KeyboardBuffer _kbFrameState = new();
    internal static Span<ButtonState> KeyboardFrameState => _kbFrameState.State;
    static int _kbPressed;

    internal static event Action<KeyboardKey>? OnKeyPressed;

    static internal void Tick()
    {
        _mouseFrameState.Reset();
        _kbFrameState.Reset();

        // todo: add handling of multikey input
        _kbPressed = Raylib.GetKeyPressed();
        if (_kbPressed > 0)
            OnKeyPressed?.Invoke((KeyboardKey)_kbPressed);
    }

    public static bool IsKeyDown(KeyboardKey button)
    {
        var state = KeyboardFrameState[(ushort)button];

        if (state >= ButtonState.Down)
            return true;
        else if (state == ButtonState.Up)
            return false;

        var now = Raylib.IsKeyDown((Raylib_cs.KeyboardKey)button);
        KeyboardFrameState[(ushort)button] = now ? ButtonState.Down : ButtonState.Up;
        return now;
    }

    public static bool IsKeyPressed(KeyboardKey button)
    {
        var state = KeyboardFrameState[(ushort)button];

        if (state == ButtonState.Pressed)
            return true;
        else if (state > ButtonState.Unknown)
            return false;

        var now = Raylib.IsKeyPressed((Raylib_cs.KeyboardKey)button);
        KeyboardFrameState[(ushort)button] = now ? ButtonState.Pressed : ButtonState.NotPressed;
        return now;
    }

    public static bool IsMouseDown(MouseButton button)
    {
        var state = MouseFrameState[(byte)button];

        if (state >= ButtonState.Down)
            return true;
        else if (state == ButtonState.Up)
            return false;

        var now = Raylib.IsMouseButtonDown((Raylib_cs.MouseButton)button);
        MouseFrameState[(byte)button] = now ? ButtonState.Down : ButtonState.Up;
        return now;
    }

    public static bool IsMousePressed(MouseButton button)
    {
        var state = MouseFrameState[(byte)button];

        if (state == ButtonState.Pressed)
            return true;
        else if (state > ButtonState.Unknown)
            return false;

        var now = Raylib.IsMouseButtonPressed((Raylib_cs.MouseButton)button);
        MouseFrameState[(byte)button] = now ? ButtonState.Pressed : ButtonState.NotPressed;
        return now;
    }

    public static bool IsMouseOver(Rectangle area)
    {
        var realPos = Raylib.GetMousePosition();
        var virtPos = Screen.ScreenToVirtual(realPos);
        return Raylib.CheckCollisionPointRec(virtPos, area);
    }

    public static bool IsMouseOver(Vector2 center, float radius)
    {
        var realPos = Raylib.GetMousePosition();
        var virtPos = Screen.ScreenToVirtual(realPos);
        return Raylib.CheckCollisionPointCircle(virtPos, center, radius);
    }

    public static Vector2 GetMousePosition()
    {
        return Screen.ScreenToVirtual(Raylib.GetMousePosition());
    }
}
