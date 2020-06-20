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
        Input.ChangeSelectCharacter(GameManager.Instance.ActiveCharacter);
        UI = Instantiate(UIPrefab);
        UI.Init(GameManager.Instance.GetPartyCharacters(), this);
        foreach (var item in GameManager.Instance.Characters)
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
        GameManager.Instance.ActiveCharacter = pChar;
        Input.ChangeSelectCharacter(GameManager.Instance.ActiveCharacter);
    }

    public void MoveCharacter(PlayerCharcater playerCharcater, Vector3 point)
    {
        playerCharcater.MoveTo(point);
        foreach (PlayerCharcater charcater in GameManager.Instance.Characters)
        {
            if (charcater != playerCharcater)
            {
                charcater.Follow(playerCharcater.gameObject);
            }
        }

    }

}
