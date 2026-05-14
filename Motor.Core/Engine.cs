using Raylib_cs;

namespace Motor.Core;

public static class Engine
{
    public static event Action? OnUpdate;
    public static event Action? OnFixedUpdate;

    public static void Run(Space space)
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.Warning);

        Screen.Init();

        while (Screen.IsOpen)
        {
            space.UpdateTransforms();
            ModifiersRegistry.UpdateAll(Raylib.GetFrameTime());

            Screen.Begin();

            ModifiersRegistry.DrawAll();

            Screen.End();
        }

        Screen.Close();
    }
}