using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : SerialisedGameObject
{
    public Dictionary<string, Action<PlayerCharcater>> InteractionActions;


    protected virtual void Awake()
    {
        InitialiseData<SD_Item>();

        InteractionActions = new Dictionary<string, Action<PlayerCharcater>>()
        {
            { "Pickup Item", PickupItem },
            { "Use Item", UseItem },
            { "Destroy Item", DestroyItem }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharcater pChar = other.GetComponent<PlayerCharcater>();
        if (pChar != null && pChar == GameManager.Instance.ActivePlayer)
        {
            GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.OpenInteractionPanel, this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.CloseInteractionPanel, this.gameObject);
    }

    public void PickupItem(PlayerCharcater playerCharcater)
    {
        Destroy(this.gameObject);
    }

    public void UseItem(PlayerCharcater playerCharcater)
    {
        ((SD_Item)ConstantData).UseItem(playerCharcater);
    }

    public void DestroyItem(PlayerCharcater playerCharcater)
    {
        throw new System.NotImplementedException();
    }
}
