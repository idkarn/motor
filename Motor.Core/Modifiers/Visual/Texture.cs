using System.Numerics;
using Raylib_cs;

namespace Motor.Core.Modifiers.Visual;

public class Texture : VisualModifierBase
{
    Texture2D? _texture;
    Raylib_cs.Rectangle _rect;
    public string Filename = "";

    public void Load(string filename)
    {
        Filename = filename;

        var img = Raylib.LoadImage(filename);

        if (_texture is Texture2D _tex)
            Raylib.UnloadTexture(_tex);

        _texture = Raylib.LoadTextureFromImage(img);
        _rect = new(0, 0, _texture.Value.Dimensions);
        Raylib.UnloadImage(img);
    }

    public override void Draw()
    {
        if (_texture is not Texture2D _tex)
            throw new Exception("Err: texture is not loaded!");

        Vector2 size = new(_rect.Width * _transform.Scale.X, _rect.Height * _transform.Scale.Y);

        Raylib_cs.Rectangle dist = new(_transform.WorldPosition - size / 2, size);

        Raylib.DrawTexturePro(_tex, _rect, dist, Vector2.Zero, _transform.Rotation, Color.White);
    }
}