using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SD_Character : SD_Base
{
    public Sprite Icon;
    public Material TestMaterial;
    public int MovementRange;
    public int Initiative;

    public override void CreateData()
    {
        base.CreateData();

    }
}
