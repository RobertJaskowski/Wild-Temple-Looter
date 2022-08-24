using UnityEngine;

public class FloatEventListener : MonoBehaviour
{
    public FloatGameEvent Event;
    public FloatUnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke(Event.Value.Value);
    }
}

