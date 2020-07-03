using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public struct BattleOrder
{
    public BaseCharacter Character;
    public int Iniative;

    public BattleOrder(BaseCharacter character, int iniative)
    {
        Character = character;
        Iniative = iniative;
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
    public List<BattleOrder> TurnOrders = new List<BattleOrder>();
    private int OrderTurn;
    public BattleOrder CurrentTurnOrder { get { return TurnOrders[OrderTurn]; } }

    public void UpdateGridHoverPosition(Vector3 vector)
    {
        Grid.UpdateHoverGridPosition(vector);
    }

    public void Awake()
    {
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.AddNPCToBattleKey, AddCharacterToBattle, this);
    }

    private void AddCharacterToBattle(GameObject npc)
    {
        NPCCharacter npcCharacter = npc.GetComponent<NPCCharacter>();
        if (!TurnOrders.Exists(x => x.Character == npcCharacter))
        {
            int rollIt = Random.Range(0, 20) + npcCharacter.CharacterDefinition.initiative;
            int newindex = TurnOrders.Count - 1;
            for (int i = TurnOrders.Count - 1; i >= 0; i--)
            {
                if (rollIt > TurnOrders[i].Iniative)
                {
                    newindex = i;
                }

            }
            if (newindex == OrderTurn)
            {
                OrderTurn++;
            }
            TurnOrders.Insert(newindex, new BattleOrder(npcCharacter, rollIt));

            Grid.AlignCharacter(npcCharacter);
            UI.Init(TurnOrders, this);
            UI.HighlightCurrentPlayer(OrderTurn);
        }
    }

    public override void Activate()
    {
        Combat_Begin();
        Input.Init(this);
        UI = Instantiate(UIPrefab);
        UI.Init(TurnOrders, this);
        Grid.Activate(this, Trigger.BattleRadius);

        Combat_BeginTurn();
    }

    public override void Deactivate()
    {
        Destroy(UI.gameObject);
        GeneralNoticeBoard.RemoveAwaitGameobject(GeneralNoticeBoard.AddNPCToBattleKey, this);
    }

    public override BaseInputManager GetActiveInput()
    {
        return Input;
    }

    public void InputClick(Vector3 hoverPoint, BaseCharacter character)
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
        if (OrderTurn >= TurnOrders.Count)
            OrderTurn = 0;
        //move camera
        Grid.GetAvailableMovementNodes(TurnOrders[OrderTurn]);
        UI.HighlightCurrentPlayer(OrderTurn);

        Input.ChangeSelectCharacter(TurnOrders[OrderTurn].Character);
    }


    public void Combat_EndTurn()
    {
        OrderTurn++;
        Combat_BeginTurn();
    }

    public void Combat_Begin()
    {
        OrderTurn = 0;
        List<BaseCharacter> Characters = new List<BaseCharacter>();
        Characters.AddRange(GameManager.Instance.Players);
        Characters.AddRange(Trigger.InludedCharacters);

        foreach (var player in Characters)
        {
            int rollIt = Random.Range(0, 20) + player.CharacterDefinition.initiative;
            TurnOrders.Add(new BattleOrder(player, rollIt));
        }
        TurnOrders = TurnOrders.OrderBy(x => x.Iniative).ToList();

    }

    public void Comabt_End()
    {
        GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.EndBattleKey, Trigger.gameObject);
    }
}
