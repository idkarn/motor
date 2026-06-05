using Motor.Core.Guards;
using Motor.Core.Serialization;

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

    internal record AreaData : ModifierData
    {
        public Transform2dModifier.TransformData Transform;
        public bool IgnoreMouse;
    }

    internal AreaData PackInto(ModifierPackingContext ctx, AreaData data)
    {
        data.IgnoreMouse = _ignoreMouse;
        data.Transform = (Transform2dModifier.TransformData)ctx.GetOrPack(_transform);
        return (PackInto(data) as AreaData)!;
    }

    internal override void InitializeFromData(ModifierData data)
    {
        base.InitializeFromData(data);

        if (data is not AreaData areaData) throw new Exception("not an Area!");

        IgnoreMouse = areaData.IgnoreMouse;
    }

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