using UnityEngine;

public class BoolEventListener : MonoBehaviour
{
    public BoolGameEvent Event;
    public BoolUnityEvent Response;

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

