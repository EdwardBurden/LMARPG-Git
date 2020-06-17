using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicEventListener : MonoBehaviour
{
    public BasicEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.Register(this);
    }

    private void OnDisable()
    {
        Event.Remove(this);
    }

    public void OnEventRasied()
    {
        Response.Invoke();
    }


}
