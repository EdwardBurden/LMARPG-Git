using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstance : MonoBehaviour
{
    [SerializeField] private AudioSource Source;
    public string AudioName = "Example";
    [SerializeField] private bool burnAfterUse;


    public float Length => Source.clip.length;
    [HideInInspector] public bool Active = false;

    public void Activate()
    {
        if (!Active)
        {
            Source.Play();
            Active = true;
        }
    }

    public void Deactivate()
    {
        if (Active)
        {
            Source.Stop();
            Active = false;
        }
    }

    private void Awake()
    {
        this.gameObject.name = AudioName;
        if (!Source.loop || burnAfterUse)
        {
            Destroy(this.gameObject, Length);
        }
    }
}
