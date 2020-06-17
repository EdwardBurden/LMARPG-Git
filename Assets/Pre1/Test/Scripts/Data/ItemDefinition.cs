using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData")]
public class ItemDefinition : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public float Weight;
    public GameObject Prefab;
}
