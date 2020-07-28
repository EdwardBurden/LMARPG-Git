using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerCharcater : BaseCharacter
{
    private SD_PlayerCharacter HiddenData;

    public override SD_Base BaseData { get => HiddenData; set => HiddenData = (SD_PlayerCharacter)value; }

    public override void Start()
    {
        base.Start();
        this.GetComponent<MeshRenderer>().material = CharacterDefinition.TestMaterial;
        this.name = CharacterDefinition.Name;
    }


}
