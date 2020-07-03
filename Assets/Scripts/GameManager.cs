
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
    [SerializeField] private NPCController NPController;

    public PlayerCharcater ActivePlayer { get { return Party.ActivePlayerCharacter; } set { Party.ActivePlayerCharacter = value; } }
    public List<PlayerCharcater> Players { get { return Party.PlayerCharacters; } }
    public List<NPCCharacter> NPCs { get { return NPController.NPCCharacters; } }

    public Camera GameCamera;

    private void Awake()
    {
        Instance = this;
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.StartBattleKey, SwitchToBattleSetup , this);
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.EndBattleKey, SwitchToRoamSetup, this);

    }

    public void Start()
    {
        ActiveSetup.Activate();
    }

    private void SwitchToBattleSetup(GameObject triggerObject)
    {
        if (ActiveSetup.GetType() != typeof(BattleSetup))
        {
            ActiveSetup.Deactivate();
            Destroy(ActiveSetup.gameObject);
            BattleSetup BSetup = GameObject.Instantiate(BattleSetup, triggerObject.transform.position, triggerObject.transform.rotation, null);
            BSetup.SetBattleTriggerSetting(triggerObject.GetComponent<BattleTrigger>());
            BSetup.Activate();
            ActiveSetup = BSetup;
        }
    }

    private void SwitchToRoamSetup(GameObject trigger)
    {
        ActiveSetup.Deactivate();
        Destroy(ActiveSetup.gameObject);
        ActiveSetup = Instantiate(RoamSetup);
        ActiveSetup.Activate();
    }

}
