using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : BaseNPCState
{
    public SeekState(NPCCharacter nPCCharacter) : base(nPCCharacter)
    {

    }

    public override void AddedToBattle()
    {
        NextType = typeof(BattleState);
    }

    public override void BattleEnded()
    {
        NextType = typeof(WanderState);
    }

    public override void StartHandle()
    {
        NextType = null;
        RoamCharacterHandler.MoveCharacterTo(Character, Character.Battle.transform.position);
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
