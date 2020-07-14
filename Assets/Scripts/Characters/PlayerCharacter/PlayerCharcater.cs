using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerCharcater : BaseCharacter
{
    public override void Start()
    {
        base.Start();
        this.GetComponent<MeshRenderer>().material = CharacterDefinition.TestMaterial;
        this.name = CharacterDefinition.Name;
    }


}
