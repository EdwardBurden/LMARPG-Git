using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameDataWindow : EditorWindow
{
    private List<SD_Character> CharacterData = new List<SD_Character>();
    private SD_PlayerCharacter Character = new SD_PlayerCharacter() { DisplayName = "Test", ID = "IdTest" };

    [MenuItem("Window/GameDataWindow")]
    static void OpenWindow()
    {
        GameDataWindow window = (GameDataWindow)GetWindow(typeof(GameDataWindow));
        window.Show();
    }

    private void OnGUI()
    {




        if (GUILayout.Button("Import"))
        {
            CharacterData = DataHandler.ImportAll<SD_Character>();
        }

        if (GUILayout.Button("Export"))
        {
            DataHandler.Export(Character);
        }
    }


    private void CreateNew<T>() where T : SD_Base
    {


    }


    private void OnEnable()
    {
        titleContent = new GUIContent("Game Data Window");
    }


}

