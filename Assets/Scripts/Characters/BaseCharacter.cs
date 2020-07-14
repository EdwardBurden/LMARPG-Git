using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseCharacter : MonoBehaviour
{
    public ParticleInstance WalkingParticlePrefab;
   
    public Transform ParticleSpawnPoint;

    public bool Follow = false;
    public GameObject FollowTarget = null;

    public CharacterDefinition CharacterDefinition;
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

    public virtual void Start()
    {
        StartCoroutine(WalkingParticle(WalkingParticlePrefab));
        //StartCoroutine(SpawnParticlesContinuously(WalkingParticlePrefab));
    }

    private IEnumerator WalkingParticle(ParticleInstance particle)
    {
        ParticleInstance inst = Instantiate(particle, ParticleSpawnPoint.position, ParticleSpawnPoint.rotation, this.transform);
        while (this.gameObject != null)
        {
            if (NavMesh.velocity.magnitude > Mathf.Epsilon)
            {
                inst.Activate();
            }
            else
            {
                inst.Deactivate();
            }

            yield return 0;

        }
    }


    private IEnumerator SpawnParticlesContinuously(ParticleInstance particle)
    {
        while (this.gameObject != null)
        {
            if (NavMesh.velocity.magnitude > Mathf.Epsilon)
            {
                ParticleInstance inst = Instantiate(particle, ParticleSpawnPoint.position, ParticleSpawnPoint.rotation, this.transform);

            }
            yield return new WaitForSeconds(particle.Length);
        }

    }
}
