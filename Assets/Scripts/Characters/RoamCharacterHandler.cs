using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoamCharacterHandler
{
    public static void UnSetFollow(BaseCharacter baseCharacter)
    {
        baseCharacter.Follow = false;
        baseCharacter.FollowTarget = null;
        baseCharacter.NavMesh.stoppingDistance = 0;
    }

    public static void SetFollow(BaseCharacter baseCharacter, GameObject gameObject)
    {
        baseCharacter.Follow = true;
        baseCharacter.FollowTarget = gameObject;
        baseCharacter.StopAllCoroutines();
        baseCharacter.NavMesh.stoppingDistance = 2;
    }

    public static void MoveCharacterTo(BaseCharacter baseCharacter, Vector3 pos)
    {
        UnSetFollow(baseCharacter);
        baseCharacter.NavMesh.SetDestination(pos);
    }

    public static void HandleUpdate_Follow(BaseCharacter baseCharacter)
    {
        if (baseCharacter.Follow && baseCharacter.FollowTarget)
        {
            baseCharacter.NavMesh.SetDestination(baseCharacter.FollowTarget.transform.position);
        }
    }


}
