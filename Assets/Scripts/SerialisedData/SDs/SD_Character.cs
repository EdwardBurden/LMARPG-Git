using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public abstract class SD_Character : SD_Base
{
    public int Level;
    [JsonProperty]
    private int[] CharacterAbilities = new int[6];
    [JsonProperty]
    private string IconPath;
    [JsonProperty]
    private string RacePath;

    [JsonIgnore]
    public RaceDefinition Race
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(RacePath))
            {
                return Resources.Load<RaceDefinition>(RacePath);
            }
            return null;
        }
        private set {
            if (value != null)
            {
                string fullpath = AssetDatabase.GetAssetPath(value.GetInstanceID());
                string[] arrays = fullpath.Split(new string[] { "Resources/" }, System.StringSplitOptions.RemoveEmptyEntries);
                if (arrays.Length > 1)
                {
                    int index = arrays[1].IndexOf(".");
                    if (index > 0)
                        arrays[1] = arrays[1].Substring(0, index);
                    RacePath = arrays[1];
                }
                else
                    RacePath = string.Empty;
            }
            else
                RacePath = string.Empty;
        }

    }


    [JsonIgnore]
    public Sprite Icon
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(IconPath))
            {
                return Resources.Load<Sprite>(IconPath);
            }
            return null;
        }
        private set
        {
            if (value != null)
            {
                string fullpath = AssetDatabase.GetAssetPath(value.GetInstanceID());
                string[] arrays = fullpath.Split(new string[] { "Resources/" }, System.StringSplitOptions.RemoveEmptyEntries);
                if (arrays.Length > 1)
                {
                    int index = arrays[1].IndexOf(".");
                    if (index > 0)
                        arrays[1] = arrays[1].Substring(0, index);
                    IconPath = arrays[1];
                }
                else
                    IconPath = string.Empty;
            }
            else
                IconPath = string.Empty;
        }
    }
    [JsonIgnore]
    private int SpendPointsStart { get { return 72 + (SpendPointsPerLevel * Level); } }
    [JsonIgnore]
    private int SpendPointsPerLevel = 1;


    public override void CreateData()
    {
        base.CreateData();

        EditorGUILayout.LabelField("Character", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        IconEditor();
        Level = EditorGUILayout.IntField("Level", Level);
        for (int i = 0; i < CharacterAbilities.Length; i++)
        {
            CharacterAbilities[i] = AbilityButton(((AbilityScoreTypes)i).ToString(), CharacterAbilities[i]);
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(10);
    }



    private void IconEditor()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Icon");
        Icon = (Sprite)EditorGUILayout.ObjectField(Icon, typeof(Sprite), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Race");
        Race = (RaceDefinition)EditorGUILayout.ObjectField(Race, typeof(ScriptableObject), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();

    }

    private int AbilityButton(string fieldname, int field)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(fieldname, GUILayout.Width(50));
        if (GUILayout.Button("-", GUILayout.Width(50)))
        {
            field -= 1;
        }
        EditorGUI.BeginDisabledGroup(true);
        field = EditorGUILayout.IntField(field, GUILayout.Width(50));
        EditorGUI.EndDisabledGroup();
        if (GUILayout.Button("+", GUILayout.Width(50)) && CharacterAbilities.Sum() < SpendPointsStart)
        {
            field++;
        }
        EditorGUILayout.EndHorizontal();
        return field;
    }

    public int GetModifier(AbilityScoreTypes abilityScore)
    {
        return CharacterAbilities[(int)abilityScore];
    }

    public int Speed { get { return Race.MovementSpeed; } } //will be from race

    public int Initaitve
    {
        get { return Random.Range(1, 20) + GetModifier(AbilityScoreTypes.Dexterity); }
    }
}
