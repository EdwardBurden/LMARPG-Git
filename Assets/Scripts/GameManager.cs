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
    public Camera GameCamera;

    private void Awake()
    {
        Instance = this;
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.BattleTriggerKey, SwitchToBattleSetup);
    }

    public void Start()
    {
        ActiveSetup.Activate();
    }

    public void SwitchToBattleSetup(GameObject triggerObject)
    {
        ActiveSetup.Deactivate();
        Destroy(ActiveSetup.gameObject);
        ActiveSetup = GameObject.Instantiate(BattleSetup, triggerObject.transform.position, triggerObject.transform.rotation, null);
        ActiveSetup.Activate();
    }

    public void SwitchToRoamSetup()
    {
        ActiveSetup.Deactivate();
        Destroy(ActiveSetup.gameObject);
        ActiveSetup = Instantiate(RoamSetup);
        ActiveSetup.Activate();
    }

    public List<PlayerCharcater> GetPartyCharacters()
    {
        return Party.PlayerCharacters;
    }
}
