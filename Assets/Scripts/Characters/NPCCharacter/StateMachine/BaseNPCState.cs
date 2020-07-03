using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNPCState
{
    protected NPCCharacter Character;
    protected Type NextType;
    public BaseNPCState(NPCCharacter character)
    {
        Character = character;
    }
    public abstract void StartHandle();
    public abstract Type UpdateHandle();

    public virtual void BattleStarted() { }
    public virtual void BattleEnded() { }
}
