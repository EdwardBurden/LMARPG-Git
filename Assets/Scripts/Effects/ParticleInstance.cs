using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInstance : MonoBehaviour
{
    [SerializeField] private ParticleSystem System;
    public string ParticleName = "Example";
    public float Length => System.main.duration;
    public bool DeleteOn1stLoop;

    public bool Active = false;

    public void Activate()
    {
        if (!Active)
        {
            System.Play();
            Active = true;
        }
    }

    public void Deactivate()
    {
        if (Active)
        {
            System.Stop();
            Active = false;
        }
    }

    private void Awake()
    {
        this.gameObject.name = ParticleName;
        if (!System.main.loop || DeleteOn1stLoop)
        {
            Destroy(this.gameObject, Length);
        }
    }
}
