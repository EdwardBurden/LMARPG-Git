using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerCharcater : BaseCharacter
{
    public SD_PlayerCharacter PlayerData;

    public override SD_Character GetCharacterData { get => PlayerData; set => PlayerData = (SD_PlayerCharacter)value; }

    public override void InitData()
    {
        PlayerData = DataHandler.ImportByName<SD_PlayerCharacter>(DataReferanceID);
    }

    public override void Start()
    {
        base.Start();
        this.GetComponent<MeshRenderer>().material = GetCharacterData.TestMaterial;
        this.name = GetCharacterData.DisplayName;
    }


}
