using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerCharcater : BaseCharacter
{
    private void Start()
    {
        this.GetComponent<MeshRenderer>().material = CharacterDefinition.TestMaterial;
        this.name = CharacterDefinition.Name;
    }


}
