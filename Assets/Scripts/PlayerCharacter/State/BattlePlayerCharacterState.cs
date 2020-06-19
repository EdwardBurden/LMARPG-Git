using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerCharacterState : BasePlayerCharacterState
{

    public BattlePlayerCharacterState(PlayerCharcater p)
    {
        Player = p;
    }

    public override bool CanMove(Vector3 position)
    {
        //todo replace with a* calculation
        float distance = Vector3.Distance(position, Player.transform.position);
        return (distance < Player.Definition.MovementRange);
    }

    public override void HandleUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public override void Move(Vector3 position)
    {
        //todo a* to target through grid
        Player.NavMesh.SetDestination(position);
    }
}
