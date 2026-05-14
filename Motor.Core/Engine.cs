using Raylib_cs;

namespace Motor.Core;

public static class Engine
{
    public static event Action? OnUpdate;
    public static event Action? OnFixedUpdate;

    public static void Run()
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.Warning);

        Screen.Init();

        while (Screen.IsOpen)
        {
            ModifiersRegistry.UpdateAll();

            Screen.Begin();

            ModifiersRegistry.DrawAll();

            Screen.End();
        }

        Screen.Close();
    }
}