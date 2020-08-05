using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamSetup : BaseSetup
{
    [SerializeField] RoamInputManager Input;
    private PartyUI PartyUI;
    private InteractionalPanelUI InteractionUI;
    [SerializeField] PartyUI PartyUIPrefab;
    [SerializeField] InteractionalPanelUI InteractionPanelUIPrefab;
    private PartyController PartyRewf;

    public void Awake()
    {
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.OpenInteractionPanel, OpenInteractionPanel, this);
        GeneralNoticeBoard.AwaitMessageGamobject(GeneralNoticeBoard.CloseInteractionPanel, CloseInteractionPanel, this);
    }

    private void OpenInteractionPanel(GameObject interactobject)
    {
        InteractableObject IObject = interactobject.GetComponent<InteractableObject>();
        CloseInteractionPanel(interactobject);
        InteractionUI = Instantiate(InteractionPanelUIPrefab, IObject.transform);
        InteractionUI.Init(IObject, GameManager.Instance.ActivePlayer);

    }


    private void CloseInteractionPanel(GameObject interactobject)
    {
        if (InteractionUI != null)
        {
            Destroy(InteractionUI.gameObject);
        }

    }

    public override void Activate()
    {
        Input.Init(this);
        Input.MoveCharcater += MoveCharacter;
        Input.MoveToInteract += MoveToInteract;
        Input.ChangeSelectCharacter(GameManager.Instance.ActivePlayer);
        PartyUI = Instantiate(PartyUIPrefab);
        PartyUI.Init(GameManager.Instance.Players, this);
        ChangeCharacter(GameManager.Instance.ActivePlayer);
    }

    public override void Deactivate()
    {
        if (PartyUI != null)
            Destroy(PartyUI.gameObject);
        if (InteractionUI != null)
            Destroy(InteractionUI.gameObject);
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

    public void MoveCharacter(BaseCharacter playerCharcater, Vector3 point)
    {
        RoamCharacterHandler.MoveCharacterTo(playerCharcater, point);
    }

    public void MoveToInteract(BaseCharacter player, InteractableObject clickedObject)
    {
        RoamCharacterHandler.MoveCharacterTo(player, clickedObject.transform.position);
    }

    private void Update()
    {
        foreach (var character in GameManager.Instance.Players)
        {
            RoamCharacterHandler.HandleUpdate_Follow(character);
        }
    }

}
