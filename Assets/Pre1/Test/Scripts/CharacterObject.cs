using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CharacterObject : MonoBehaviour
{
    [HideInInspector]
    public Vector3 Destination;

    [SerializeField]
    private NavMeshAgent Agent;

    private CharacterController _Controller;
    public CharacterController Controller { get { return _Controller; } }
    public Action<CharacterController> OnClick;
    public delegate CharacterController GetCharacter();
    public event GetCharacter GetSelectedCharacterEvent;

    public void Init(CharacterController characterController)
    {
        Agent.speed = characterController.GetCharacterSpeed();
        _Controller = characterController;
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

    private void FollowSelected()
    {
        CharacterController selectd = GetSelectedCharacterEvent();
        if (selectd != null && selectd != Controller)
            Destination = GetSelectedCharacterEvent().CObject.transform.position;

    }

    private void Update()
    {
        FollowSelected();
        Agent.SetDestination(Destination);
    }

}
