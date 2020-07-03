using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleCharacterHandler
{
    public static void MoveCharacterTo(BaseCharacter baseCharacter, Vector3 pos, Vector2 arraypos)
    {
        baseCharacter.GridArrayPosition = arraypos;
        baseCharacter.NavMesh.SetDestination(pos);
    }
}
