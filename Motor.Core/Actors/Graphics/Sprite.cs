using System.Numerics;
using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

public class Sprite : Graphic<Texture>
{
    public string Filename
    {
        get => GetModifier<Texture>()!.Filename;
        set => GetModifier<Texture>()!.Load(value);
    }
}