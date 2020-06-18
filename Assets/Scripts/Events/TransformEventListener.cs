using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;
using System;


[Serializable]
public class TransformUnityEvent : UnityEvent<Transform>
{
}

public class TransformEventListener : MonoBehaviour
{
    public TransformEvent Event;
    public TransformUnityEvent Response;

    private void OnEnable()
    {
        Event.Register(this);
    }

    private void OnDisable()
    {
        Event.Remove(this);
    }

    public void OnEventRasied(Transform responseTransform)
    {
        Response.Invoke(responseTransform);
    }


}
