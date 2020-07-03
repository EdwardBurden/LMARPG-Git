using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseCharacter : MonoBehaviour
{
    public bool Follow = false;
    public GameObject FollowTarget = null;

    public Vector2 GridArrayPosition;

    [SerializeField] private NavMeshAgent _NavMesh;
    public NavMeshAgent NavMesh
    {
        get
        {
            if (_NavMesh == null)
                _NavMesh = GetComponent<NavMeshAgent>();
            return _NavMesh;
        }
    }
}
