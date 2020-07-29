using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class SD_Base
{
    public string ID;
    public string DisplayName;

    public virtual void CreateData()
    {
        EditorGUILayout.BeginVertical();
        ID = EditorGUILayout.TextField(ID);
        DisplayName = EditorGUILayout.TextField(DisplayName);
        EditorGUILayout.EndVertical();
    }
}
