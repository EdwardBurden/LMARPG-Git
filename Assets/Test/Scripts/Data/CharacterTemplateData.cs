using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData")]
public class CharacterTemplateData : ScriptableObject
{
    public string CharacterName;
    public CharacterObject Prefab;
    [Range(1, 10)]
    public float Speed; //would be generated in future from character stats
    public List<ItemDefinition> StartingItems;
}
