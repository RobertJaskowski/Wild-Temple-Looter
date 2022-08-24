using UnityEngine;

public class DropTableEventListener : MonoBehaviour
{
    public DropTableGameEvent Event;
    public DropTableUnityEvent Response;

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
