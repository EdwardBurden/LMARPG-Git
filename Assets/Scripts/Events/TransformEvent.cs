using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/TransformEvent")]
public class TransformEvent : ScriptableObject
{
    private List<TransformEventListener> listeners = new List<TransformEventListener>();

    public void Raise(Transform transform)
    {
        foreach (TransformEventListener listener in listeners)
        {
            listener.OnEventRasied(transform);
        }
    }

    public void Register(TransformEventListener eventListener)
    {
        listeners.Add(eventListener);
    }

    public void Remove(TransformEventListener eventListener)
    {
        listeners.Remove(eventListener);
    }
}
