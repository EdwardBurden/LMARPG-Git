using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInputManager : MonoBehaviour
{

    [SerializeField] protected Texture2D SuccessTexture;
    [SerializeField] protected Texture2D FailureTexture;
    protected PlayerCharcater SelectedPlayerCharacter;

    public abstract void ChangeSelectCharacter(PlayerCharcater newCharacter);

    protected abstract void HandleHover();

    protected abstract void HandleClick();

    private void Update()
    {
        HandleHover();
        HandleClick();
    }

}
