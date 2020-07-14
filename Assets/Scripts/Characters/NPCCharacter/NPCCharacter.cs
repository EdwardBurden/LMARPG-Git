using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : BaseCharacter
{
    public Transform HomePoint;
    public BattleTrigger Battle { get; private set; }

    public NPCStateMachine Statemachine;

    private void Awake()
    {
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.StartBattleKey, BattleStarted, this);
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.EndBattleKey, BattleEnded, this);
        // GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.AddNPCToBattleKey, AddedToBattle, this);
        InitStateMachine();
    }

    public void AddedToBattle()
    {
        if (Battle != null)
        {
           // Statemachine.CurrentState.AddedToBattle();
            Statemachine.SwitchState(typeof(BattleState));
        }
    }

    private void BattleStarted(GameObject battletrigger)
    {
        if (Battle == null)
        {
            Battle = battletrigger.GetComponent<BattleTrigger>();

            Statemachine.CurrentState.BattleStarted();
        }
    }

    private void BattleEnded(GameObject battletrigger)
    {
        Battle = null;
        Statemachine.CurrentState.BattleEnded();
    }

    public void InitStateMachine()
    {
        var startes = new Dictionary<Type, BaseNPCState>()
        {
            {typeof(WanderState), new WanderState(this) },
            {typeof(FleeState), new FleeState(this) },
            {typeof(SeekState), new SeekState(this) },
            { typeof(BattleState) , new BattleState(this)}
        };
        Statemachine.InitStateMachine(startes, typeof(WanderState));
    }

}
