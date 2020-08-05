using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SD_Item : SD_Base
{
    public float InteractionDistance;
    public float Weight;
    public float Value;

    public override void CreateData()
    {
        base.CreateData();
    }

    public abstract void UseItem(PlayerCharcater playerCharacter);
}
