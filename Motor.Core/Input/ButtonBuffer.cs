namespace Motor.Core.Input;

abstract class ButtonBuffer<TKey>
    where TKey : struct, Enum
{
    protected ButtonState[] _states = new ButtonState[Enum.GetValues<TKey>().Length];
    internal Span<ButtonState> State => _states;

    internal void Reset() => Array.Fill(_states, ButtonState.Unknown);
}