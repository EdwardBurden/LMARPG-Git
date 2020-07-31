using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateNewDataWindow : EditorWindow
{
    private static SD_Base DataModel;

    [MenuItem("Window/GameDataWindow")]
    public static void OpenWindow<T>() where T : SD_Base
    {
        CreateNewDataWindow window = (CreateNewDataWindow)GetWindow(typeof(CreateNewDataWindow));
        DataModel = Activator.CreateInstance<T>();
        window.Show();
    }

    private void OnGUI()
    {
        if (DataModel != null)
            DataModel.CreateData();

        if (GUILayout.Button("Add"))
        {
            bool isfilled = DataModel.ValidateModel();
            if (isfilled)
            {
                DataHandler.Export(DataModel);

            }
        }
    }

}


