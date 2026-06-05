using Motor.Core.Guards;
using Motor.Core.Modifiers.Visual;

namespace Motor.Core.Actors.Graphics;

[RegisterRole("Sprite")]
public class Sprite : Graphic<Texture>
{
    public string Filename
    {
        get => GetModifier<Texture>()!.Filename;
        set => GetModifier<Texture>()!.Load(value);
    }

    public Sprite(bool isEmpty) : base(isEmpty) { }
    public Sprite() : base() { }
}