using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterDefinition : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public Material TestMaterial;
    public int MovementRange;
    [Range(0, 10)]
    public int initiative;
}
