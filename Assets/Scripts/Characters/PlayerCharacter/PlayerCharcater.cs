using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerCharcater : BaseCharacter
{
    public PlayerCharacterDefinition Definition;
    private void Start()
    {
        this.GetComponent<MeshRenderer>().material = Definition.TestMaterial;
        this.name = Definition.Name;
    }


}
