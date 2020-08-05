using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class SD_Character : SD_Base
{
    public string IconPath;
    public int MovementRange;
    public int Initiative;

    private Sprite EditorIcon;

    public override void CreateData()
    {
        base.CreateData();

        EditorGUILayout.LabelField("Character", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Icon");

        if (!string.IsNullOrWhiteSpace(IconPath))
        {
            EditorIcon = Resources.Load<Sprite>(IconPath);
        }

        EditorIcon = (Sprite)EditorGUILayout.ObjectField(EditorIcon, typeof(Sprite), allowSceneObjects: true);
        if (EditorIcon != null)
        {
            string fullpath = AssetDatabase.GetAssetPath(EditorIcon.GetInstanceID());
            string[] arrays = fullpath.Split(new string[] { "Resources/" }, System.StringSplitOptions.RemoveEmptyEntries);
            if (arrays.Length > 1)
            {
                int index = arrays[1].IndexOf(".");
                if (index > 0)
                    arrays[1] = arrays[1].Substring(0, index);
                IconPath = arrays[1];
            }
        }
        else
            IconPath = string.Empty;

        EditorGUILayout.EndHorizontal();
        MovementRange = (int)EditorGUILayout.Slider("MovementRange", MovementRange, 1, 10);
        Initiative = EditorGUILayout.IntField("Initiative", Initiative);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(10);
    }
}
