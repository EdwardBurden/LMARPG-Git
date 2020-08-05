using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SD_PlayerCharacter : SD_Character
{
    public string Greeting;

    public override void CreateData()
    {
        base.CreateData();
        EditorGUILayout.LabelField("Player Data", EditorStyles.boldLabel);
        Greeting = EditorGUILayout.TextField("Greeting", Greeting);
    }
}
