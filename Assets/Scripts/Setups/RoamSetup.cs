using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamSetup : BaseSetup
{
    [SerializeField] RoamInputManager Input;
    private PartyUI UI;
    [SerializeField] PartyUI UIPrefab;
    private PartyController PartyRewf;

    public override void Activate()
    {
        Input.Init(this);
        Input.MoveCharcater += MoveCharacter;
        Input.ChangeSelectCharacter(Manager.ActiveCharacter);
        UI = Instantiate(UIPrefab);
        UI.Init(Manager.GetPartyCharacters(), this);
        foreach (var item in Manager.Characters)
        {
            item.ChangeToRoam();
        }
    }

    public override void Deactivate()
    {
        Destroy(UI.gameObject);
    }

    public override BaseInputManager GetActiveInput()
    {
        return Input;
    }

    public void ChangeCharacter(PlayerCharcater pChar)
    {
        Manager.ActiveCharacter = pChar;
        Input.ChangeSelectCharacter(Manager.ActiveCharacter);
    }

    public void MoveCharacter(PlayerCharcater playerCharcater, Vector3 point)
    {
        playerCharcater.MoveTo(point);
        foreach (PlayerCharcater charcater in Manager.Characters)
        {
            if (charcater != playerCharcater)
            {
                charcater.Follow(playerCharcater.gameObject);
            }
        }

    }

}
