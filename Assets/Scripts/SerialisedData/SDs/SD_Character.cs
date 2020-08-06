using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class SD_Character : SD_Base
{
    public int Level;
    public int Strength = 8;
    public int Dexterity = 8;
    public int Constitution = 8;
    public int Intelligence = 8;
    public int Wisdom = 8;
    public int Charisma = 8;
    public string IconPath;

    private Sprite EditorIcon;
    private int SpendPointsStart = 72;
    private int SpendPointsPerLevel;

    public override void CreateData()
    {
        base.CreateData();

        EditorGUILayout.LabelField("Character", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        Icon();
        Strength = AbilityButton("St", Strength);
        Dexterity = AbilityButton("Dex", Dexterity);
        Constitution = AbilityButton("Con", Constitution);
        Intelligence = AbilityButton("Int", Intelligence);
        Wisdom = AbilityButton("Wis", Wisdom);
        Charisma = AbilityButton("Cha", Charisma);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(10);
    }

    private void Icon()
    {
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

    }

    private int AbilityButton(string fieldname, int field)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(fieldname , GUILayout.Width(50));
        if (GUILayout.Button("-", GUILayout.Width(50)))
        {
            field -= 1;
        }
        EditorGUI.BeginDisabledGroup(false);
        field = EditorGUILayout.IntField(field, GUILayout.Width(50));
        EditorGUI.EndDisabledGroup();
        if (GUILayout.Button("+", GUILayout.Width(50)) && GetPointsSpent() < SpendPointsStart)
        {
            field++;
        }
        EditorGUILayout.EndHorizontal();
        return field;
    }

    private int GetPointsSpent()
    {
        return Strength + Dexterity + Constitution + Intelligence + Wisdom + Charisma;

    }
}
