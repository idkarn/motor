using System.Numerics;
using Motor.Core.Actors.UI;
using Motor.Core.Input;
using Motor.Core.Modifiers.Area;
using Motor.Core.Modifiers.Controller;

class ButtonController : Controller<TextureButton>
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
        // if (isMouseOver || dragged)
        //     GetModifier<Texture>()!.Color = Color16.Red;
        // else
        //     GetModifier<Texture>()!.Color = Color16.Green;

        if (!dragged && isMouseOver && Input.IsMouseDown(MouseButton.Left))
        {
            dragged = true;
            // Actor.Text = "uwu";
            shift = Transform.Position - Input.GetMousePosition();
            // GetModifier<Texture>()!.Color = Color16.Red;
        }
        else if (dragged && !Input.IsMouseDown(MouseButton.Left))
        {
            dragged = false;
            // Actor.Text = "drag me";
            // GetModifier<Texture>()!.Color = Color16.Green;
        }
        if (dragged)
            Transform.Position = Input.GetMousePosition() + shift;
    }
}