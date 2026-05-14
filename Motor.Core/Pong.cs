using System.Numerics;
using Raylib_cs;
using Motor.Core;


class Pong
{
    Vector2 ballPos = new Vector2(Screen.Width / 2, Screen.Height / 2);
    Vector2 ballVel = new Vector2(60, 40);
    Rectangle paddleL = new Rectangle(5, Screen.Height / 2 - 10, 4, 20);
    Rectangle paddleR = new Rectangle(Screen.Width - 9, Screen.Height / 2 - 10, 4, 20);
    float ballRadius = 3.0f;
    public void Update()
    {
        float dt = Raylib.GetFrameTime();

        // 4. Input & Movement
        if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.W)) paddleL.Y -= 80 * dt;
        if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.S)) paddleL.Y += 80 * dt;

        // Simple AI for Right Paddle
        if (Random.Shared.NextSingle() > 0.2)
            if (MathF.Abs(ballPos.Y - paddleR.Y) > 10) paddleR.Y += 50 * dt;
            else paddleR.Y -= 50 * dt;

        // 5. Physics (AABB & Ball)
        ballPos += ballVel * dt;

        // Bounce Top/Bottom
        if (ballPos.Y < 0 || ballPos.Y > Screen.Height) ballVel.Y *= -1;

        // Paddle Collisions
        if (Raylib.CheckCollisionCircleRec(ballPos, ballRadius, paddleL) ||
            Raylib.CheckCollisionCircleRec(ballPos, ballRadius, paddleR))
        {
            ballVel.X *= -1.1f; // Speed up slightly on hit
        }
    }
    public void Draw()
    {
        Raylib.DrawRectangleRec(paddleL, Color.White);
        Raylib.DrawRectangleRec(paddleR, Color.White);
        Raylib.DrawCircleV(ballPos, ballRadius, Color.White);

        // Mid-line
        Raylib.DrawLine(Screen.Width / 2, 0, Screen.Width / 2, Screen.Height, Color.DarkGray);
    }
}