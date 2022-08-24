using UnityEngine;

public class IntEventListener : MonoBehaviour
{
    public IntGameEvent Event;
    public IntUnityEvent Response;

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
