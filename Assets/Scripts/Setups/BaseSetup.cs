using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSetup : MonoBehaviour
{
    protected GameManager Manager;

    public virtual void Init(GameManager gameManager)
    {
        Manager = gameManager;
    }

    public abstract void Activate();
    public abstract void Deactivate();
    public abstract BaseInputManager GetActiveInput();
}
