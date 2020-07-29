using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SerialisedGameObject))]
public abstract class SerialisedGameObjectEditor<T> : Editor where T : SD_Base
{
    T MyT;
    SerializedProperty Id;

    private void OnEnable()
    {
        Id = serializedObject.FindProperty("DataReferanceID");
        MyT = DataHandler.ImportByName<T>(Id.stringValue);
    }

    public abstract void OpenNew();

    public virtual void SelectData()
    {
        if (MyT == null)
        {
            EditorGUILayout.LabelField("Select Data");
            List<T> datas = DataHandler.ImportAll<T>();
            EditorGUILayout.BeginVertical();
            foreach (var item in datas)
            {
                if (GUILayout.Button(item.DisplayName))
                {
                    MyT = item;
                    Id.stringValue = item.ID;
                }
            }
            EditorGUILayout.EndVertical();
        }
        else
        {
            EditorGUILayout.LabelField("Selected Data = " + MyT.DisplayName);
            if (GUILayout.Button("Reset"))
            {
                MyT = null;
                Id.stringValue = null;
            }
        }
        if (GUILayout.Button("New"))
        {
            OpenNew();
        }
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        SelectData();
        serializedObject.ApplyModifiedProperties();
    }
}