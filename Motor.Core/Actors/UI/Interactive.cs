using Motor.Core.Modifiers.Area;

namespace Motor.Core.Actors.UI;

public abstract class Interactive<TArea> : Actor
    where TArea : Area2d, new()
{
    readonly TArea _area;
    public event Action? MouseEnter
    {
        add => _area.OnMouseEnter += value;
        remove => _area.OnMouseEnter -= value;
    }
    public event Action? MouseExit
    {
        add => _area.OnMouseExit += value;
        remove => _area.OnMouseExit -= value;
    }
    public event Action? Click
    {
        add => _area.OnClick += value;
        remove => _area.OnClick -= value;
    }
    public event Action? MouseDown
    {
        add => _area.OnMouseDown += value;
        remove => _area.OnMouseDown -= value;
    }
    public event Action? MouseUp
    {
        add => _area.OnMouseUp += value;
        remove => _area.OnMouseUp -= value;
    }

    public Interactive()
    {
        _area = new TArea();
        AddModifier(_area);
    }
}