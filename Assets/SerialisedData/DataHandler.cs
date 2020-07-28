using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public static class DataHandler
{
    private const string ProjectAssetRoute = "Assets/Definitions/SD";
    private const string Extension = ".sd";

    private static T Import<T>(string targetPath) where T : SD_Base //change to data base
    {
        string jsonString = File.ReadAllText(targetPath);
        return JsonConvert.DeserializeObject<T>(jsonString);
    }

    public static T ImportByName<T>(string id, bool saveToProject) where T : SD_Base //change to data base
    {
        string path = Path.Combine(ProjectAssetRoute, typeof(T).ToString(), id);
        return Import<T>(path);
    }

    public static List<T> ImportAll<T>() where T : SD_Base //change to data base
    {
        string path = Path.Combine(ProjectAssetRoute, typeof(T).ToString());
        List<T> data = new List<T>();
        foreach (var filePath in Directory.GetFiles(path))
        {
            if (Path.HasExtension(Extension) && filePath.EndsWith(Extension))
                data.Add(Import<T>(filePath));
        }
        return data;
    }

    public static void Export<T>(T exportData) where T : SD_Base //change to data base
    {
        string path = Path.Combine(ProjectAssetRoute, typeof(T).ToString());
        Directory.CreateDirectory(path);
        string assetname = exportData.ID + Extension;
        path = Path.Combine(path, assetname);
        string jsondata = JsonConvert.SerializeObject(exportData);
        File.WriteAllText(path, jsondata);
    }


}
