using Motor.Core.Modifiers.Area;

namespace Motor.Core.Actors.UI;

public abstract class Interactive<TArea> : Actor
    where TArea : Area2d, new()
{
    Area2d _area // todo: make inline as everywhere
    {
        get => GetModifier<Area2d>()!;
    }
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

    public Interactive(bool isEmpty) : base(isEmpty)
    {
        if (!isEmpty)
            AddModifier(new TArea());
    }
    public Interactive() : this(false) { }
}