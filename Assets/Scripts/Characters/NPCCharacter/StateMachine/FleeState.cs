using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseNPCState
{
    public FleeState(NPCCharacter nPCCharacter) : base(nPCCharacter)
    {

    }

    public override void BattleEnded()
    {
        NextType = typeof(WanderState);
    }

    public override void StartHandle()
    {
        NextType = null;
        RoamCharacterHandler.MoveCharacterTo(Character, Character.HomePoint.position);
    }

    public override Type UpdateHandle()
    {
        if (NextType != null)
        {
            return NextType;
        }
        return null;
    }
}
