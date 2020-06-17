using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/BasicEvent")]
public class BasicEvent : ScriptableObject
{
    private List<BasicEventListener> listeners = new List<BasicEventListener>();

    public void Raise()
    {
        foreach (BasicEventListener listener in listeners)
        {
            listener.OnEventRasied();
        }
    }

    public void Register(BasicEventListener eventListener)
    {
        listeners.Add(eventListener);
    }

    public void Remove(BasicEventListener eventListener)
    {
        listeners.Remove(eventListener);
    }
}
