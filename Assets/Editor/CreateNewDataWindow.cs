using System;
using UnityEngine;

public class CreateNewDataWindow : DataWindow
{
    public static void OpenNewWindow<T>(SerialisedGameObject serialisedGameObject) where T : SD_Base
    {
        CreateNewDataWindow window = (CreateNewDataWindow)GetWindow(typeof(CreateNewDataWindow));
        DataModel = Activator.CreateInstance<T>();
        Target = serialisedGameObject;
        window.Show();
    }

    protected override void DrawWindow()
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


