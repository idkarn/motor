using Raylib_cs;
using System.Numerics;

class Doom
{
    // 1. Setup Resolutions
    const int screenWidth = 800;
    const int screenHeight = 600;
    const int vResW = 160; // Ultra low-res for that Pico feel
    const int vResH = 120;
    Vector3 enemyPos = new(9.0f, 0.5f, 9.0f);
    Texture2D enemyTex;
    // 3. Player / Camera Setup
    Camera3D camera = new()
    {
        Position = new Vector3(2.0f, 1.0f, 2.0f), // Player height at 1.0
        Target = new Vector3(3.0f, 1.0f, 3.0f),
        Up = Vector3.UnitY,
        FovY = 60.0f,
        Projection = CameraProjection.Perspective
    };
    bool isShooting = false;
    float shootTimer = 0f;
    float muzzleFlashTimer = 0f;

    float enemyRadius = 0.5f;

    // 4. Simple Map (Walls)
    readonly Rectangle[] walls = [
        new(0, 0, 10, 1),   // North Wall
        new(0, 0, 1, 10),   // West Wall
        new(9, 0, 1, 10),   // East Wall
        new(0, 9, 10, 1),   // South Wall
        new(4, 4, 2, 2)     // Center Pillar
    ];
    public Doom()
    {
        Raylib.DisableCursor(); // Lock mouse to center

        Image enemyImg = Raylib.GenImageChecked(8, 8, 1, 1, Color.Green, Color.Lime);
        enemyTex = Raylib.LoadTextureFromImage(enemyImg);
        Raylib.UnloadImage(enemyImg);
    }
    public void Update()
    {
        // 1. Store the position BEFORE moving
        Vector3 oldPos = camera.Position;

        // 2. Let Raylib move the camera based on input
        Raylib.UpdateCamera(ref camera, CameraMode.FirstPerson);
        camera.Position.Y = 1.0f; // Keep on ground

        // 3. Define the player's collision "bubble"
        Vector2 newPos2D = new Vector2(camera.Position.X, camera.Position.Z);
        float playerRadius = 0.3f;

        foreach (var wall in walls)
        {
            if (Raylib.CheckCollisionCircleRec(newPos2D, playerRadius, wall))
            {
                // 4. Collision Detected! 
                // Try to keep X move but reset Z
                Vector3 testX = new Vector3(camera.Position.X, 1.0f, oldPos.Z);
                if (!Raylib.CheckCollisionCircleRec(new Vector2(testX.X, testX.Z), playerRadius, wall))
                {
                    camera.Position = testX;
                }
                else
                {
                    // Try to keep Z move but reset X
                    Vector3 testZ = new Vector3(oldPos.X, 1.0f, camera.Position.Z);
                    if (!Raylib.CheckCollisionCircleRec(new Vector2(testZ.X, testZ.Z), playerRadius, wall))
                    {
                        camera.Position = testZ;
                    }
                    else
                    {
                        // Both blocked? Total stop.
                        camera.Position = oldPos;
                    }
                }
            }
        }

        // 1. Check for Input
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            isShooting = true;
            muzzleFlashTimer = 0.1f; // Flash for 0.1 seconds

            // 2. RAYCASTING (The "Bullet")
            // Get a ray from the center of the screen
            Ray ray = new Ray(camera.Position, Vector3.Normalize(camera.Target - camera.Position));

            // Check if ray hits enemy (Collision Sphere)
            RayCollision hit = Raylib.GetRayCollisionSphere(ray, new Vector3(enemyPos.X, 0.5f, enemyPos.Y), enemyRadius);

            if (hit.Hit)
            {
                Console.WriteLine("HIT ENEMY!");
                // Add recoil/shake here
            }
        }

        float dt = Raylib.GetFrameTime();

        // 3. Muzzle Flash Timer
        if (muzzleFlashTimer > 0) muzzleFlashTimer -= dt;

        enemyPos += Vector3.Normalize(camera.Position - enemyPos) * dt;
    }

    public void Draw()
    {
        Raylib.BeginMode3D(camera);
        // Floor and Ceiling
        Raylib.DrawPlane(new Vector3(5, 0, 5), new Vector2(10, 10), Color.DarkGray);

        // Draw Walls as Cubes
        foreach (var wall in walls)
        {
            Vector3 pos = new(wall.X + wall.Width / 2, 1.0f, wall.Y + wall.Height / 2);
            Raylib.DrawCube(pos, wall.Width, 2.0f, wall.Height, Color.Gray);
            Raylib.DrawCubeWires(pos, wall.Width, 2.0f, wall.Height, Color.White); // Pico wireframes
        }

        Raylib.DrawBillboard(camera, enemyTex, enemyPos, 1.0f, Color.White);

        int gunX = vResW / 2 - 10;
        int gunY = vResH - 25;

        // The "Gun" is just a rectangle in this example
        Raylib.DrawRectangle(gunX, gunY, 20, 30, Color.DarkGray);

        // Draw Muzzle Flash
        if (muzzleFlashTimer > 0)
        {
            Raylib.DrawCircle(vResW / 2, vResH / 2 + 10, 5, Color.Yellow);
        }

        Raylib.EndMode3D();

        // Simple Crosshair
        Raylib.DrawCircle(vResW / 2, vResH / 2, 1, Color.Red);
    }
}