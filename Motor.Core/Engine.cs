namespace Motor.Core;

public enum EngineState
{
    Uninitialized,
    Initializing,
    Prepared,
    Loading,
    Ready,
    Running,
    Paused,
    Stopping,
    Stopped
}

public static class Engine
{
    public static event Action? OnUpdate;
    public static event Action? OnFixedUpdate;
    static Game _game = null!;
    public static EngineState State { get; private set; } = EngineState.Uninitialized;

    public static void Init()
    {
        State = EngineState.Initializing;

        Raylib_cs.Raylib.SetTraceLogLevel(Raylib_cs.TraceLogLevel.Warning);

        Screen.Init();

        State = EngineState.Prepared;
    }

    public static void Load(Game game)
    {
        if (State != EngineState.Prepared)
            throw new Exception("Err: engine is not prepared yet!");

        State = EngineState.Loading;

        _game = game;

        State = EngineState.Ready;
    }

    public static void Start()
    {
        if (State != EngineState.Ready)
            throw new Exception("Err: engine is not ready yet!");

        State = EngineState.Running;

        _game.MainScene.UpdateTransforms();
        ModifiersRegistry.StartAll();

        while (State == EngineState.Running)
        {
            _game.MainScene.UpdateTransforms();

            Input.InputManager.Tick();

            OnUpdate?.Invoke();

            ModifiersRegistry.UpdateAll(Raylib_cs.Raylib.GetFrameTime());

            Screen.Begin();

            ModifiersRegistry.DrawAll();

            Screen.End();

            if (!Screen.IsOpen) Shutdown();
        }

        Screen.Close();
    }

    public static void Shutdown()
    {
        State = EngineState.Stopping;

        Console.WriteLine("[NG] Stopped");

        State = EngineState.Stopped;
    }
}