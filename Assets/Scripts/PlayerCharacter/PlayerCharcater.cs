using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharcater : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NavMesh;
    public PlayerCharacterDefinition Definition;
    bool Follow;
    GameObject FollowTarget;

    private void Start()
    {
        this.GetComponent<MeshRenderer>().material = Definition.TestMaterial;
        this.name = Definition.Name;
    }

    private void Update()
    {
        //roam, move to state system
        if (Follow && FollowTarget)
        {
            NavMesh.SetDestination(FollowTarget.transform.position);
        }

    }

    public void UnSetFollow()
    {
        Follow = false;
        FollowTarget = null;
        NavMesh.stoppingDistance = 0;
    }

    public void SetFollow(GameObject gameObject)
    {
        Follow = true;
        FollowTarget = gameObject;
        NavMesh.stoppingDistance = 2;
    }

    public void MoveTo(Vector3 dest)
    {
        UnSetFollow();
        NavMesh.SetDestination(dest);
    }
}
