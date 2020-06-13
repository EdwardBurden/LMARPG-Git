using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CharacterObject : MonoBehaviour
{
    public float MinDistance;
    public float MaxDistance;


    [HideInInspector]
    public Vector3 Destination;

    [SerializeField]
    private NavMeshAgent Agent;

    private CharacterController Controller;

    public Action<CharacterController> OnClick;

    public void Init(CharacterController characterController)
    {
        Agent.speed = characterController.GetCharacterSpeed();
        Controller = characterController;
    }

    private void Start()
    {
        Destination = this.transform.position;
    }

    private void OnMouseUpAsButton()
    {
        Destination = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        Agent.SetDestination(Destination);
        OnClick(Controller);
    }


}
