using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class SD_Character : SD_Base
{
    public string IconPath;
    public Material TestMaterial;
    public int MovementRange;
    public int Initiative;

    private Sprite EditorIcon;

    public override void CreateData()
    {
        base.CreateData();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Icon");

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

    }
}
