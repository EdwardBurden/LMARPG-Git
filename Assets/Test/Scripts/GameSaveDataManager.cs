using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameSaveDataManager 
{
    private const string DataPathFolder = "Party";

    public static bool SaveGame(GameSaveData data, string savename)
    {
        try
        {
            string folderPath = GetFolderPath();
            string filePath = Path.Combine(folderPath, savename);//put in later save name enter
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static GameSaveData LoadGame(string name)
    {
        try
        {
            string folderPath = GetFolderPath();
            string filePath = Path.Combine(folderPath, name);
            if (File.Exists(filePath))
            {

                string textObject = File.ReadAllText(filePath);
                GameSaveData saveFile = JsonUtility.FromJson<GameSaveData>(textObject);
                if (saveFile != null)
                {
                    return saveFile;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private static string GetFolderPath()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, DataPathFolder);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        return folderPath;
    }




}
