using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] private ParticleInstance WalkingParticlePrefab = null; //could change in future for different terrain, might have to be list/restructured
    [SerializeField] private AudioInstance WalkingSoundPrefab = null; //could change in future for different terrain, might have to be list/restructured

    [SerializeField] private Transform ParticleSpawnPoint = null;
    [SerializeField] private CharacterDefinition _CharacterDefinition;

    [HideInInspector] public bool Follow = false;
    [HideInInspector] public GameObject FollowTarget = null;
    [HideInInspector] public Vector2 GridArrayPosition;

    public CharacterDefinition CharacterDefinition { get { return _CharacterDefinition; } }

    /*[SerializeField]*/
    private NavMeshAgent _NavMesh;
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
        StartCoroutine(Walking(WalkingParticlePrefab, WalkingSoundPrefab));
    }

    private IEnumerator Walking(ParticleInstance walkingParticle, AudioInstance walkingsound)
    {
        ParticleInstance particleObject = Instantiate(walkingParticle, ParticleSpawnPoint.position, ParticleSpawnPoint.rotation, this.transform);
        AudioInstance audioObject = Instantiate(walkingsound, this.transform);

        while (this.gameObject != null)
        {
            if (NavMesh.velocity.magnitude > Mathf.Epsilon) // replace with RoamHandler.IsCharacterMoving
            {
                particleObject.Activate();
                audioObject.Activate();
            }
            else
            {
                particleObject.Deactivate();
                audioObject.Deactivate();
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
