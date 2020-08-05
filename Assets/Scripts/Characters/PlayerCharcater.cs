using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerCharcater : BaseCharacter
{
    public override SD_Character GetCharacterData { get => (SD_PlayerCharacter)ConstantData; set => ConstantData = (SD_PlayerCharacter)value; }

    private void Awake()
    {
        InitialiseData<SD_PlayerCharacter>();
    }


    public override void Start()
    {
        base.Start();
        this.name = GetCharacterData.DisplayName;
    }


}
