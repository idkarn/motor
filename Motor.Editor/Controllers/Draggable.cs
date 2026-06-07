using System.Numerics;
using Motor.Core.Actors.UI;
using Motor.Core.Input;
using Motor.Core.Modifiers.Controller;
using Motor.Core.Modifiers.Area;

namespace Motor.Editor.Controllers;

class Draggable<TArea> : Controller<RectArea>
    where TArea : Area2d, new()
{
    bool isMouseOver = false;
    bool dragged = false;
    Vector2 shift;

    void Start()
    {
        GetModifier<Area2d>()!.IgnoreMouse = false;
        Actor.MouseEnter += () => isMouseOver = true;
        Actor.MouseExit += () => isMouseOver = false;
    }

    void Update(float dt)
    {
        if (!dragged && isMouseOver && Input.IsMouseDown(MouseButton.Left))
        {
            dragged = true;
            shift = Transform.Position - Input.GetMousePosition();
        }
        else if (dragged && !Input.IsMouseDown(MouseButton.Left))
        {
            dragged = false;
        }
        if (dragged)
            Transform.Position = Input.GetMousePosition() + shift;
    }
}

class DraggableRect : Draggable<Rectangle> { }