using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaseSetup ActiveSetup;
    [SerializeField] private RoamSetup RoamSetup;
    [SerializeField] private BattleSetup BattleSetup;
    [SerializeField] public PlayerCharcater ActivePlayerCharacter;
    [SerializeField] public List<PlayerCharcater> PlayerCharacters;

    public void Start()
    {
        ActiveSetup.Init(this);
        ActiveSetup.Activate();
    }

    public void SwitchToBattleSetup(Transform triggertransform)
    {
        ActiveSetup.Deactivate();
        Destroy(ActiveSetup.gameObject);
        ActiveSetup = GameObject.Instantiate(BattleSetup, triggertransform.position, triggertransform.rotation, null);
        ActiveSetup.Init(this);
        ActiveSetup.Activate();
    }



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            SwitchToRoamSetup();

        }
    }

    public void SwitchToRoamSetup()
    {
        ActiveSetup.Deactivate();
        Destroy(ActiveSetup.gameObject);
        ActiveSetup = Instantiate(RoamSetup);
        ActiveSetup.Init(this);
        ActiveSetup.Activate();
    }
}
