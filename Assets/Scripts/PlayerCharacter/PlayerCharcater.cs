using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerCharcater : MonoBehaviour
{
    public NavMeshAgent NavMesh;
    public PlayerCharacterDefinition Definition;
    private BasePlayerCharacterState State;
    private Vector3 NodePosition;

    private void Start()
    {
        State = new RoamPlayerCharacterState(this); //todo move toa init call
        this.GetComponent<MeshRenderer>().material = Definition.TestMaterial;
        this.name = Definition.Name;
    }

    public void ChangeToRoam()
    {
        State = new RoamPlayerCharacterState(this);
    }

    public void ChangeToBattle()
    {
        State = new BattlePlayerCharacterState(this);
    }

    private void Update()
    {
        State.HandleUpdate();
    }

    public void MoveTo(Vector3 pos)
    {
        State.Move(pos);
        NodePosition = pos;
    }

    public Vector3 GetPosition()
    {
        return NodePosition;
    }

    public void Follow(GameObject leader)
    {
        if ((RoamPlayerCharacterState)State != null)
            ((RoamPlayerCharacterState)State).SetFollow(leader);
    }

    public bool CanMoveTo(Vector3 centrePosition)
    {
        return State.CanMove(centrePosition);
    }
}
