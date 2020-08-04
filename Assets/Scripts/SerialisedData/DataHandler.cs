using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public static class DataHandler
{
    private const string ProjectAssetRoute = "Assets/Definitions/SD";
    private const string Extension = ".sd";
    private static JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All, TypeNameAssemblyFormat = FormatterAssemblyStyle.Full };

    private static T Import<T>(string targetPath) where T : SD_Base //change to data base
    {
        string jsonString = File.ReadAllText(targetPath);
        var desObject = JsonConvert.DeserializeObject(jsonString, settings);
        return (T)desObject;
    }


    public static T SearchAndImportByName<T>(string id) where T : SD_Base //change to data base
    {
        T returnValue = null;
        Type providedtype = typeof(T);
        foreach (var item in Utils.GetDerivedTypes(providedtype))
        {
            string folderpath = Path.Combine(ProjectAssetRoute, item.ToString());
            if (Directory.Exists(folderpath))
            {
                string path = Path.Combine(ProjectAssetRoute, item.ToString(), id + Extension);
                if (File.Exists(path))
                {
                    returnValue = Import<T>(path);
                }
            }

        }
        return returnValue;
    }

    public static T ImportByName<T>(string id) where T : SD_Base //change to data base
    {
        string path = Path.Combine(ProjectAssetRoute, typeof(T).ToString(), id + Extension);
        if (File.Exists(path))
        {
            return Import<T>(path);
        }
        else
        {
            return null;
        }
    }

    public static List<T> ImportAll<T>() where T : SD_Base //change to data base
    {
        List<T> data = new List<T>();
        Type providedtype = typeof(T);
        foreach (var item in Utils.GetDerivedTypes(providedtype))
        {
            string path = Path.Combine(ProjectAssetRoute, item.ToString());
            if (Directory.Exists(path))
            {
                foreach (var filePath in Directory.GetFiles(path))
                {
                    if (Path.HasExtension(Extension) && filePath.EndsWith(Extension))
                        data.Add(Import<T>(filePath));
                }
            }
        }

        return data;

    }

    public static void Export<T>(T exportData) where T : SD_Base //change to data base
    {
        string path = Path.Combine(ProjectAssetRoute, exportData.GetType().ToString());
        Directory.CreateDirectory(path);
        string assetname = exportData.ID + Extension;
        path = Path.Combine(path, assetname);
        string jsondata = JsonConvert.SerializeObject(exportData, settings);
        File.WriteAllText(path, jsondata);
    }


}
