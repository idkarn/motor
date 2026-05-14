using System.Numerics;
using Raylib_cs;

class Cube
{
    readonly string resourcesPath = Path.Combine(AppContext.BaseDirectory, "data", "resources");
    float rotationAng = 0;
    Camera3D camera = new(
        new Vector3(4, 4, 4),
        Vector3.Zero,
        Vector3.UnitY,
        45.0f,
        CameraProjection.Perspective
    );
    Model model;
    public Cube()
    {
        model = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(2.0f, 2.0f, 2.0f));
        // Texture2D texture = Raylib.LoadTexture("resources/texture.png");
        Image img = Raylib.GenImageChecked(64, 64, 8, 8, Color.Blue, Color.White);
        Texture2D texture = Raylib.LoadTextureFromImage(img);
        Raylib.UnloadImage(img);
        Raylib.SetTextureFilter(texture, TextureFilter.Point);

        Shader shader = Raylib.LoadShader(Path.Combine(resourcesPath, "pico.vert"), Path.Combine(resourcesPath, "pico.frag"));

        unsafe
        {
            model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;
            model.Materials[0].Shader = shader;
        }
    }
    public void Update()
    {
        rotationAng += Raylib.GetFrameTime() * 1.0f;

        Matrix4x4 rotation = Matrix4x4.CreateRotationY(rotationAng);
        model.Transform = rotation;
    }
    public void Draw()
    {
        Raylib.BeginMode3D(camera);

        // 3. Draw a "crunchy" wireframe cube
        // We draw it slightly offset to simulate vertex jitter
        // Raylib.DrawCubeWires(Vector3.Zero, 2.0f, 2.0f, 2.0f, Color.White);
        Raylib.DrawModel(model, Vector3.Zero, 1.0f, Color.White);
        Raylib.DrawGrid(10, 1.0f);

        Raylib.EndMode3D();
    }
}