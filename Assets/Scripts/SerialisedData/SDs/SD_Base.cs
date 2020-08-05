using System;
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
        EditorGUILayout.LabelField("BaseData", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Id");
        ID = EditorGUILayout.TextField(ID);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Display Name");
        DisplayName = EditorGUILayout.TextField(DisplayName);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(10);
    }

    public virtual bool ValidateModel()
    {
        return !(string.IsNullOrWhiteSpace(ID) || string.IsNullOrWhiteSpace(DisplayName));
    }
}
