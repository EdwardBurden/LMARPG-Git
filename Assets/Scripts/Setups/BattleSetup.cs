using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSetup : BaseSetup
{
    [SerializeField] BattleInputManager Input;
    [SerializeField] BattleGrid Grid;
    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        Input.Init(this);
    }

    public void UpdateGridHoverPosition(Vector3 vector)
    {
        Grid.UpdateHoverGridPosition(vector);
    }

    public override void Activate()
    {
        Grid.Activate(Manager.PlayerCharacters);
        Input.ChangeSelectCharacter(Manager.ActivePlayerCharacter);
    }

    public override void Deactivate()
    {
        //  throw new System.NotImplementedException();
    }

    public override BaseInputManager GetActiveInput()
    {
        return Input;
    }

    public void MoveCharcaterToGridPosition(Vector3 hoverPoint, PlayerCharcater character)
    {
        BattleNode node = Grid.GetNodeAtWorldPosition(hoverPoint);
        character.MoveTo(node.CentrePosition);
    }
}
