using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerCharacterState
{
    protected PlayerCharcater Player;
    public abstract void Move(Vector3 position);
    public abstract bool CanMove(Vector3 position);

    public abstract void HandleUpdate();
}
