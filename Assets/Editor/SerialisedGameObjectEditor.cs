using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SerialisedGameObject))]
public abstract class SerialisedGameObjectEditor<T> : Editor where T : SD_Base
{
    bool showSelection = false;

    SerializedProperty SelectedId;
    string Name;
    T _Data;
    T SelectedData
    {
        get
        {
            if (_Data == null)
                _Data = DataHandler.SearchAndImportByName<T>(SelectedId.stringValue);
            return _Data;
        }
        set { _Data = value; }
    }

    private void OnEnable()
    {
        SelectedId = serializedObject.FindProperty("DataReferanceID");
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
                if (GUILayout.Button(item.DisplayName + ":" + item.GetType().ToString()))
                {
                    SelectedId.stringValue = item.ID;
                    showSelection = false;
                    SelectedData = null;
                }
            }
            EditorGUILayout.EndVertical();
        }
    }

    private void Clear()
    {
        SelectedId.stringValue = null;
    }

    public virtual void DisplayOptions()
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Data", EditorStyles.largeLabel);
        string display = SelectedData == null ? "No Data Selected" : SelectedData.DisplayName;
        EditorGUILayout.LabelField(display);
        EditorGUILayout.BeginVertical();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Show/Hide Selection"))
        {
            showSelection = !showSelection;
        }
        if (GUILayout.Button("New"))
        {
            OpenNew();
            showSelection = false;
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("View"))
        {
            OpenView(SelectedData);
            showSelection = false;
        }
        if (GUILayout.Button("Clear"))
        {
            Clear();
            showSelection = false;
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