using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharcater : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NavMesh;


    public void MoveTo(Vector3 dest)
    {
        NavMesh.SetDestination(dest);
    }
}
