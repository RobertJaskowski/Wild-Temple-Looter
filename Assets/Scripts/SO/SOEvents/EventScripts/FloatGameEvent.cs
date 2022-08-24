using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/FloatGameEvent")]
public class FloatGameEvent : ScriptableObject
{
    public FloatReference Value;

    private List<FloatEventListener> listeners = new List<FloatEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(FloatEventListener listener)
    {
        listeners.Add(listener);
    }


    public void UnregisterListener(FloatEventListener listener)
    {
        listeners.Remove(listener);
    }

    public FloatGameEvent SetValue(float value)
    {
        Value.Value = value;
        return this;
    }


    public FloatGameEvent UpdateValue(float amount)
    {

        Value.Value += amount;
        return this;
    }

    public FloatGameEvent UpdateValueAndRaise(float amount)
    {
        Value.Value += amount;
        Raise();
        return this;
    }


}
