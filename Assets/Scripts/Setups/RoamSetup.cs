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
        Input.ChangeSelectCharacter(GameManager.Instance.ActivePlayer);
        UI = Instantiate(UIPrefab);
        UI.Init(GameManager.Instance.GetPartyCharacters(), this);
        ChangeCharacter(GameManager.Instance.ActivePlayer);
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
        GameManager.Instance.ActivePlayer = pChar;
        Input.ChangeSelectCharacter(GameManager.Instance.ActivePlayer);
        foreach (PlayerCharcater charcater in GameManager.Instance.Players)
        {
            if (charcater != pChar)
            {
                RoamCharacterHandler.SetFollow(charcater, pChar.gameObject);
            }
        }
    }

    public void MoveCharacter(PlayerCharcater playerCharcater, Vector3 point)
    {
        RoamCharacterHandler.MoveCharacterTo(playerCharcater, point);
    }

    private void Update()
    {
        foreach (var character in GameManager.Instance.Players)
        {
            RoamCharacterHandler.HandleUpdate_Follow(character);
        }
    }

}
