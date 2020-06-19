using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private BaseSetup ActiveSetup;
    [SerializeField] private RoamSetup RoamSetup;
    [SerializeField] private BattleSetup BattleSetup;
    [SerializeField] private PartyController Party;
    public PlayerCharcater ActiveCharacter { get { return Party.ActivePlayerCharacter; } set { Party.ActivePlayerCharacter = value; } }
    public List<PlayerCharcater> Characters { get { return Party.PlayerCharacters; } set { Party.PlayerCharacters = value; } }

    private void Awake()
    {
        Instance = this;
    }

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

    public List<PlayerCharcater> GetPartyCharacters()
    {
        return Party.PlayerCharacters;
    }
}
