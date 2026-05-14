using Motor.Core.Guards;
using Raylib_cs;

namespace Motor.Core.Modifiers;

public abstract class ModifierBase
{
    public bool IsEnabled { get; set; } = true;
    internal virtual void Update() { }
}

public abstract class VisualModifierBase : ModifierBase, IDrawable
{
    public virtual void Draw()
    {
        throw new NotImplementedException();
    }
}

public abstract class ShapeVisualModifier : VisualModifierBase
{
    [InjectModifier]
    protected Transform2dModifier _transform = null!;
    protected Color _rayColor;
    public Color16 Color
    {
        get => Color;
        set
        {
            var clr = value.ToColor();
            _rayColor = new Color(clr.R, clr.G, clr.B, clr.A);
        }
    }

    public ShapeVisualModifier()
    {
        Color = Color16.Green;
    }
}

internal interface IDrawable
{
    void Draw();
}