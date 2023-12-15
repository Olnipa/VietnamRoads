using System;

public abstract class MainParameterModel
{
    public int Value { get; protected set; }

    public event Action Updated;

    public void AddValue(int valueToAdd)
    {
        if (valueToAdd <= 0)
            return;

        Value += valueToAdd;
        Updated.Invoke();
    }

    public bool TryRemoveValue(int valueToRemove)
    {
        if (Value >= valueToRemove)
        {
            Value -= valueToRemove;
            Updated.Invoke();
            return true;
        }

        return false;
    }
}