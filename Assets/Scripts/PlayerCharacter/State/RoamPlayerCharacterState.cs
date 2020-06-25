using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamPlayerCharacterState : BasePlayerCharacterState
{
    bool Follow = false;
    GameObject FollowTarget = null;

    public RoamPlayerCharacterState(PlayerCharcater p)
    {
        Player = p;
    }

    public override void HandleUpdate()
    {
        if (Follow && FollowTarget)
        {
            Player.NavMesh.SetDestination(FollowTarget.transform.position);
        }
    }

    public void UnSetFollow()
    {
        Follow = false;
        FollowTarget = null;
        Player.NavMesh.stoppingDistance = 0;
    }

    public void SetFollow(GameObject gameObject)
    {
        Follow = true;
        FollowTarget = gameObject;
        Player.NavMesh.stoppingDistance = 2;
    }

    public override void Move(Vector3 position)
    {
        UnSetFollow();
        Player.NavMesh.SetDestination(position);
    }

    public override bool CanMove(Vector3 position)
    {
        return true;
    }

}
