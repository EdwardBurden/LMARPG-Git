using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseNPCState> AvailableStates;
    public BaseNPCState CurrentState { get; private set; }
    public Action<BaseNPCState> StateChanged;

    public void InitStateMachine(Dictionary<Type, BaseNPCState> states, Type starting)
    {
        AvailableStates = states;
        SwitchState(starting);
    }

    private void Update()
    {
        Type next = CurrentState?.UpdateHandle();

        if (next != null && next != CurrentState.GetType())
        {
            SwitchState(next);
        }
    }

    private void SwitchState(Type type)
    {
        CurrentState = AvailableStates[type];
        CurrentState.StartHandle();
        StateChanged?.Invoke(CurrentState);
    }
}
