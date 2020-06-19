using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Definition/PlayerCharacter")]
public class PlayerCharacterDefinition : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public Material TestMaterial;
}
