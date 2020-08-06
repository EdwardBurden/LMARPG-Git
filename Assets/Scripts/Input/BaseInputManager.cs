using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInputManager : MonoBehaviour
{
    [SerializeField] protected LayerMask FloorDetectionTarget;
    [SerializeField] protected Texture2D SuccessTexture;
    [SerializeField] protected Texture2D FailureTexture;
    protected BaseCharacter SelectedPlayerCharacter;

    public abstract void ChangeSelectCharacter(BaseCharacter newCharacter);

    protected abstract void HandleHover();

    protected abstract void HandleClick();

    protected virtual void Update()
    {
        HandleHover();
        HandleClick();
    }

  

}
