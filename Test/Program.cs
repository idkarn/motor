using System.Numerics;
using Raylib_cs;


class Body(Vector<double> initPos, Vector<double> initVec, double mass)
{
    public Vector<double> Pos { get; private set; } = initPos;
    public Vector<double> Vel { get; private set; } = initVec;
    public Vector<double> Accel = new(Sim.zero);
    public double Mass = mass;
    public Color Color = new((byte)Raylib.GetRandomValue(0, 255), (byte)Raylib.GetRandomValue(0, 255), (byte)Raylib.GetRandomValue(0, 255), (byte)255);
    public readonly Queue<Vector<double>> trailHistory = new();
    const int maxTrailLength = 300; // Number of trail frames to keep
    float waypointTime = 0;

    public void Update(float dt)
    {
        Vel += Accel * dt;
        Pos += Vel * dt;

        waypointTime -= dt;
        if (waypointTime > 0) return;
        trailHistory.Enqueue(Pos);
        if (trailHistory.Count > maxTrailLength)
            trailHistory.Dequeue();
        waypointTime = 0.1f;
    }
}

class Sim
{
    const double G = 6.67430e-11;
    public static readonly double[] zero = [0, 0, 0, 0];
    public readonly List<Body> Bodies = [];

    public void AddBody(Body body)
    {
        Bodies.Add(body);
    }

    public void Update(float dt)
    {
        foreach (var body in Bodies)
            UpdateForceFor(body);
        foreach (var body in Bodies)
            body.Update(dt);
    }

    void UpdateForceFor(Body body)
    {
        Vector<double> sum = new(zero);
        Vector<double> d;
        double l;
        foreach (var b in Bodies.Where(b => b != body))
        {
            d = b.Pos - body.Pos;
            l = Math.Sqrt(d[0] * d[0] + d[1] * d[1]);
            sum += b.Mass * d / Math.Pow(l, 3);
        }
        body.Accel = sum * G;
    }
}

class Program
{
    static void Main(string[] args)
    {
        const int screenWidth = 800;
        const int screenHeight = 450;
        const float dotRadius = 8f;
        const float padding = 10f;

        Raylib.InitWindow(screenWidth, screenHeight, "sim");
        Raylib.SetTargetFPS(60);

        Camera2D camera = new()
        {
            Target = new Vector2(0, 0),
            Offset = new Vector2(screenWidth / 2.0f, screenHeight / 2.0f),
            Rotation = 0.0f,
            Zoom = 2.0f
        };

        // *** SIMULATION SETUP ***

        Sim sim = new();

        Body b1 = new(new(Sim.zero), new(new double[] { 1, 0, 0, 0 }), 10e15);
        Body b2 = new(new(new double[] { 100.0, 100.0, 0, 0 }), new(new double[] { 0, 50.0, 0, 0 }), 10e3);
        Body b3 = new(new(new double[] { 30.0, -90.0, 0, 0 }), new(new double[] { -40.0, 0, 0, 0 }), 10e1);

        sim.AddBody(b1);
        sim.AddBody(b2);
        sim.AddBody(b3);

        // Main loop

        float t = 0;
        float speed = 1;
        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime() * speed;
            t += deltaTime;

            sim.Update(deltaTime);

            for (int i = 0; i < sim.Bodies.Count; i++)
                Console.WriteLine($"#{i}: <{sim.Bodies[i].Pos}>");

            double minX = 0, minY = 0, maxX = 0, maxY = 0;
            foreach (var body in sim.Bodies)
            {
                minX = Math.Min(minX, body.Pos[0]);
                minY = Math.Min(minY, body.Pos[1]);
                maxX = Math.Max(maxX, body.Pos[0]);
                maxY = Math.Max(maxY, body.Pos[1]);
            }

            camera.Target = new Vector2((float)((minX + maxX) / 2f), (float)((minY + maxY) / 2f));
            camera.Zoom = MathF.Min(10.0f, (float)Math.Min(screenWidth / (maxX - minX + (padding * 2f)), screenHeight / (maxY - minY + (padding * 2f))));

            if (Raylib.IsKeyDown(KeyboardKey.Down))
                speed -= Raylib.GetFrameTime();
            else if (Raylib.IsKeyDown(KeyboardKey.Up))
                speed += Raylib.GetFrameTime();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.BeginMode2D(camera);

            foreach (var body in sim.Bodies)
            {
                Vector<double>[] points = [.. body.trailHistory];

                if (points.Length >= 4)
                    Raylib.DrawSplineCatmullRom([.. points.Select(x => new Vector2((float)x[0], (float)x[1]))], points.Length, 1.0f, body.Color);

                Raylib.DrawCircle((int)body.Pos[0], (int)body.Pos[1], (float)Math.Log10(dotRadius * body.Mass), Color.Maroon);
            }

            Raylib.EndMode2D();

            Raylib.DrawText($"speed:{speed}", 20, 20, 20, Color.Green);
            Raylib.DrawText($"t:{t}", 20, 40, 20, Color.Green);
            Raylib.DrawText($"zoom:{camera.Zoom}", 20, 60, 20, Color.Green);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
