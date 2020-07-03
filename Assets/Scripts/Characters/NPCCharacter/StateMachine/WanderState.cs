using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderState : BaseNPCState
{
    private Vector3 Destination;
    private float WanderWaitTime = 4.0f;
    private float WaitTimer;
    private float WanderRange = 5.0f;

    public WanderState(NPCCharacter nPCCharacter) : base(nPCCharacter)
    {

    }

    public override void BattleStarted()
    {
        CheckDistanceToBattle();
    }

    public override void StartHandle()
    {
        NextType = null;
    }

    public override Type UpdateHandle()
    {
        WaitTimer += Time.deltaTime;
        if (WaitTimer > WanderWaitTime)
        {
            Destination = new Vector3()
            {
                x = Random.Range(-WanderRange, WanderRange),
                y = 0,
                z = Random.Range(-WanderRange, WanderRange)
            };
            Character.NavMesh.SetDestination(Destination);
            WaitTimer = 0;
        }
        CheckDistanceToBattle();
        if (NextType != null)
        {
            return NextType;
        }
        return null;
    }

    private void CheckDistanceToBattle()
    {
        if (Character.Battle && Character.Battle.EffectRadius > Vector3.Distance(Character.transform.position, Character.Battle.gameObject.transform.position))
        {
            if (((NPCCharacterDefinition)Character.CharacterDefinition).Aggressiveness > Character.Battle.AggressionLevel)
                NextType = typeof(SeekState);
            else
                NextType = typeof(FleeState);
        }
    }
}
