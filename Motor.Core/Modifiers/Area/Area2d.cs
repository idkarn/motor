using Motor.Core.Guards;

namespace Motor.Core.Modifiers.Area;

public abstract class Area2d : ModifierBase
{
    [InjectModifier]
    protected Transform2dModifier _transform = null!;

    bool _isMouseOver = false;
    bool _ignoreMouse = true;
    public bool IgnoreMouse
    {
        get => _ignoreMouse;
        set
        {
            if (value == _ignoreMouse) return;
            _ignoreMouse = value;
            if (_ignoreMouse)
                Engine.OnUpdate -= Update;
            else
                Engine.OnUpdate += Update;
        }
    }
    public event Action? OnMouseEnter;
    public event Action? OnMouseExit;
    public event Action? OnMouseDown;
    public event Action? OnMouseUp;
    public event Action? OnClick;

    public abstract bool IsMouseOver();

    protected virtual void Update()
    {
        var isMouseOverNow = IsMouseOver();
        if (isMouseOverNow != _isMouseOver)
        {
            if (_isMouseOver)
                OnMouseExit?.Invoke();
            else
                OnMouseEnter?.Invoke();
            _isMouseOver = isMouseOverNow;
        }

        if (_isMouseOver && Input.InputManager.IsMousePressed(Input.MouseButton.Left))
            OnClick?.Invoke();
    }
}