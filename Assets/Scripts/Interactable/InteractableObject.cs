using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : SerialisedGameObject
{
    public SD_Item ItemData;

    public override void InitData()
    {
        ItemData = DataHandler.SearchAndImportByName<SD_Item>(DataReferanceID);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    public void Pickup(PlayerCharcater playerCharcater)
    {
        Destroy(this.gameObject);
    }

    public void UseItem(PlayerCharcater playerCharcater)
    {
        ItemData.UseItem(playerCharcater);
    }

}
