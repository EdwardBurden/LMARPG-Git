using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation 
{
    public string Name;
    public float Speed;

    public CharacterInformation(string characterName, float speed)
    {
        Name = characterName;
        Speed = speed;
    }
}
