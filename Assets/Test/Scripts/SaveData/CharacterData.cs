using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public string Name;
    public Vector3 Position;
    public string PrefabString;
    public float Speed; //would be generated in future from character stats

    public CharacterData(string name, Vector3 position, string prefabString, float speed)
    {
        Name = name;
        Position = position;
        PrefabString = prefabString;
        Speed = speed;
    }
}
