using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : SerialisedGameObject
{
    protected virtual void Awake()
    {
        InitialiseData<SD_Item>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharcater pChar = other.GetComponent<PlayerCharcater>();
        if (pChar != null)
        {
            //spawnUI
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //destryui
    }

    public void Pickup(PlayerCharcater playerCharcater)
    {
        Destroy(this.gameObject);
    }

    public void UseItem(PlayerCharcater playerCharcater)
    {
        ((SD_Item)ConstantData).UseItem(playerCharcater);
    }

}
