using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SerialisedGameObject))]
public abstract class SerialisedGameObjectEditor<T> : Editor where T : SD_Base
{
    bool showSelection = false;
    T SelectedData;
    SerializedProperty SelectedId;

    private void OnEnable()
    {
        SelectedId = serializedObject.FindProperty("DataReferanceID");
        SelectedData = DataHandler.ImportByName<T>(SelectedId.stringValue);
    }

    public abstract void OpenNew();

    public abstract void OpenView(T myT);

    private void Select()
    {
        if (showSelection)
        {
            EditorGUILayout.LabelField("Select Data", EditorStyles.largeLabel);

            List<T> datas = DataHandler.ImportAll<T>();
            EditorGUILayout.BeginVertical();
            foreach (var item in datas)
            {
                if (GUILayout.Button(item.DisplayName))
                {
                    SelectedData = item;
                    SelectedId.stringValue = item.ID;
                }
            }
            EditorGUILayout.EndVertical();
        }
    }

    private void Clear()
    {
        SelectedData = null;
        SelectedId.stringValue = null;
    }

    public virtual void DisplayOptions()
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Data", EditorStyles.largeLabel);
        EditorGUILayout.BeginVertical();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Show/Hide Selection"))
        {
            showSelection = !showSelection;
        }
        if (GUILayout.Button("New"))
        {
            OpenNew();
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("View"))
        {
            OpenView(SelectedData);
        }
        if (GUILayout.Button("Clear"))
        {
            Clear();
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.EndVertical();
        Select();
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        DisplayOptions();
        serializedObject.ApplyModifiedProperties();
    }
}