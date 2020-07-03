using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public struct BattleOrder
{
    public PlayerCharcater Character;
    public int Iniative;
    public int Stamina;

    public BattleOrder(PlayerCharcater character, int iniative)
    {
        Character = character;
        Iniative = iniative;
        Stamina = 3;
    }
}


public class BattleSetup : BaseSetup
{
    private BattleTrigger Trigger;
    [SerializeField] BattleInputManager Input;
    [SerializeField] BattleGrid Grid;
    private TurnOrderUI UI;
    [SerializeField] TurnOrderUI UIPrefab;

    //battle manager
    public List<BattleOrder> PartyOrder = new List<BattleOrder>();
    private int PlayerTurn;

    public void UpdateGridHoverPosition(Vector3 vector)
    {
        Grid.UpdateHoverGridPosition(vector);
    }

    public override void Activate()
    {
        Input.Init(this);
        UI = Instantiate(UIPrefab);

        Grid.Activate(GameManager.Instance.GetPartyCharacters() , Trigger.BattleRadius);

        Combat_Begin();
        UI.Init(PartyOrder, this);



        Combat_BeginTurn();
    }

    public override void Deactivate()
    {
        Destroy(UI.gameObject);
    }

    public override BaseInputManager GetActiveInput()
    {
        return Input;
    }

    public void InputClick(Vector3 hoverPoint, PlayerCharcater character)
    {
        if (Grid.CanMoveCharcaterOnGrid(character, hoverPoint))
        {
            Grid.MoveCharacterOnGrid(character, hoverPoint);
            //put wait here later
            Combat_EndTurn();
        }
    }

    public void SetBattleTriggerSetting(BattleTrigger battleTrigger)
    {
        Trigger = battleTrigger;
    }

    public void Combat_BeginTurn()
    {
        if (PlayerTurn >= PartyOrder.Count)
            PlayerTurn = 0;
        //move camera
        Grid.GetAvailableMovementNodes(PartyOrder[PlayerTurn]);
        UI.HighlightCurrentPlayer(PlayerTurn);
        PlayerCharcater PC = PartyOrder[PlayerTurn].Character;
        Input.ChangeSelectCharacter(PC);
    }


    public void Combat_EndTurn()
    {
        PlayerTurn++;
        Combat_BeginTurn();
    }

    public void Combat_Begin()
    {
        PlayerTurn = 0;
        foreach (var player in GameManager.Instance.Players)
        {
            int rollIt = Random.Range(0, 20) + player.Definition.initiative;
            PartyOrder.Add(new BattleOrder(player, rollIt));
        }
        PartyOrder = PartyOrder.OrderBy(x => x.Iniative).ToList();

    }

    public void Comabt_End()
    {
        GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.EndBattleKey, Trigger.gameObject);
    }
}
