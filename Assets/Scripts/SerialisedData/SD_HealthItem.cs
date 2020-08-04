using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_HealthItem : SD_Item
{
    public float HealthRecovered;

    public override void UseItem(PlayerCharcater playerCharacter)
    {
        playerCharacter.Health += HealthRecovered;
    }

    public override void CreateData()
    {
        //base.CreateData();
    }
}
