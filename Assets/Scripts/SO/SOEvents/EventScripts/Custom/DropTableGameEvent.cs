using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Custom/DropTableGameEvent")]
public class DropTableGameEvent : ScriptableObject
{
    public DropTableReference Value;

    private List<DropTableEventListener> listeners = new List<DropTableEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(DropTableEventListener listener)
    {
        listeners.Add(listener);
    }


    public void UnregisterListener(DropTableEventListener listener)
    {
        listeners.Remove(listener);
    }

    public DropTableGameEvent SetValue(DropTable value)
    {
        Value.Value = value;
        return this;
    }


    public DropTableGameEvent SetValueAndRaise(DropTable value)
    {
        Value.Value = value;
        Raise();
        return this;
    }


}
