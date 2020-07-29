using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SerialisedGameObject : MonoBehaviour
{
    [HideInInspector][SerializeField] public string DataReferanceID;

    public abstract void InitData();

    protected virtual void Awake()
    {
        InitData();
    }
}
