using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/BoolGameEvent")]
public class BoolGameEvent : ScriptableObject
{
    public BoolReference Value;

    private List<BoolEventListener> listeners = new List<BoolEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(BoolEventListener listener)
    {
        listeners.Add(listener);
    }


    public void UnregisterListener(BoolEventListener listener)
    {
        listeners.Remove(listener);
    }

    public BoolGameEvent SetValue(bool value)
    {
        Value.Value = value;
        return this;
    }

    public BoolGameEvent SetValueAndRaise(bool value)
    {
        Value.Value = value;
        Raise();
        return this;
    }
    


}
