using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

public class Sprite(bool isEmpty) : Graphic<Texture>(isEmpty)
{
    public string Filename
    {
        get => GetModifier<Texture>()!.Filename;
        set => GetModifier<Texture>()!.Load(value);
    }
}