namespace Motor.Core.Modifiers.Area;


// todo: replace this thing with common Circle
public class CircleArea : Area2d
{
    public int Radius { get; set; } = 20;

    public override bool IsMouseOver()
    {
        return Input.InputManager.IsMouseOver(_transform.WorldPosition, Radius);
    }
}