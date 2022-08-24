using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/IntGameEvent")]
public class IntGameEvent : ScriptableObject
{
    public IntReference Value;

    private List<IntEventListener> listeners = new List<IntEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(IntEventListener listener)
    {
        listeners.Add(listener);
    }


    public void UnregisterListener(IntEventListener listener)
    {
        listeners.Remove(listener);
    }

    public IntGameEvent SetValue(int value)
    {
        Value.Value = value;
        return this;
    }


    public IntGameEvent UpdateValue(int amount)
    {
        Value.Value += amount;
        return this;
    }

    public IntGameEvent UpdateValueAndRaise(int amount)
    {
        Value.Value += amount;
        Raise();
        return this;
    }


}
