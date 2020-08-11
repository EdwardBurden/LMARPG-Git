using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class CreateNewDataWindow : DataWindow
{
    static bool TypeSelection = false;
    static Type BaseType;

    static List<Type> DerivedTypes = new List<Type>();
    int SelectedClass = -1;

    public static void OpenNewWindow<T>(SerialisedGameObject serialisedGameObject) where T : SD_Base
    {
        CreateNewDataWindow window = (CreateNewDataWindow)GetWindow(typeof(CreateNewDataWindow));
        DataModel = Activator.CreateInstance<T>();
        TypeSelection = false;
        Target = serialisedGameObject;
        window.Show();
    }

    public static void OpenNewWindow(SerialisedGameObject serialisedGameObject, Type typeselect)
    {
        CreateNewDataWindow window = (CreateNewDataWindow)GetWindow(typeof(CreateNewDataWindow));
        BaseType = typeselect;
        TypeSelection = true;
        DerivedTypes = StaticFunctions.GetDerivedTypes(BaseType);
        Target = serialisedGameObject;
        window.Show();
    }

    protected override void DrawWindow()
    {
        if (TypeSelection && SelectedClass == -1)
        {
            int newtype = EditorGUILayout.Popup(SelectedClass, DerivedTypes.Select(x => x.Name).ToArray());
            if (newtype != SelectedClass)
            {
                DataModel = (SD_Base)Activator.CreateInstance(DerivedTypes[newtype]);
                SelectedClass = newtype;
            }
        }
        else
        {
            base.DrawWindow();
            if (GUILayout.Button("Add"))
            {
                Add();
                Close();
            }
            if (GUILayout.Button("Add & Assign"))
            {
                Add();
                Assign();
                Close();
            }
        }
    }
}


