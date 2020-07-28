using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerCharcater), true)]
public class PlayerCharcaterEditor : Editor
{
    PlayerCharcater Custom;

    void OnEnable()
    {
        Custom = (PlayerCharcater)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (Custom.BaseData == null)
        {
            EditorGUILayout.LabelField("Select Data");
            List<SD_PlayerCharacter> datas = DataHandler.ImportAll<SD_PlayerCharacter>();
            EditorGUILayout.BeginVertical();
            foreach (var item in datas)
            {
                if (GUILayout.Button(item.DisplayName))
                {
                    Custom.BaseData = item;
                }
            }
            EditorGUILayout.EndVertical();
        }
        else {
            EditorGUILayout.LabelField("Select Data = "+ Custom.BaseData.DisplayName);


        }
    }
}
