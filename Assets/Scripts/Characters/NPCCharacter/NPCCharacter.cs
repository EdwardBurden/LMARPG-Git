using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : BaseCharacter
{
    public NPCCharacterDefinition Definition;
    public Transform HomePoint;
    public BattleTrigger Battle { get; private set; }

    public NPCStateMachine Statemachine;

    private void Awake()
    {
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.StartBattleKey, BattleStarted);
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.EndBattleKey, BattleEnded);
        InitStateMachine();
    }

    private void BattleStarted(GameObject battletrigger)
    {
        Battle = battletrigger.GetComponent<BattleTrigger>();
        Statemachine.CurrentState.BattleStarted();
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
            {typeof(SeekState), new SeekState(this) }
        };
        Statemachine.InitStateMachine(startes, typeof(WanderState));
    }

}
