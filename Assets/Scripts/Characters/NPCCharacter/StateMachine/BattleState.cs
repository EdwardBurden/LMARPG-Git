using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : BaseNPCState
{
    public BattleState(NPCCharacter nPCCharacter) : base(nPCCharacter) { }

    public override void StartHandle()
    {
        NextType = null;
    }

    public override void BattleEnded()
    {
        NextType = typeof(WanderState);
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
