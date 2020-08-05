using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ViewDataWindow : DataWindow
{
    public static void OpenViewWindow(SerialisedGameObject serialisedGameObject, SD_Base data)
    {
        ViewDataWindow window = (ViewDataWindow)GetWindow(typeof(ViewDataWindow));
        DataModel = data;
        Target = serialisedGameObject;
        window.Show();
    }

    protected override void DrawWindow()
    {
        base.DrawWindow();
        if (GUILayout.Button("Save"))
        {
            Add();
            Close();
        }
    }


}
