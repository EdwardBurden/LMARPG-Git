using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneralNoticeBoard
{
    public  const string StartBattleKey = "StartBattle";
    public const string EndBattleKey = "EndBattle";
    private static Dictionary<string, ReadingList<GameObject>> GameObjectDictionary = new Dictionary<string, ReadingList<GameObject>>();


    private class ReadingList<T>
    {
        public T MessageParam;
        public List<Action<T>> Readers;

        public ReadingList(T param)
        {
            MessageParam = param;
            Readers = new List<Action<T>>();
        }
    }


    public static void PostMessageGameobject(string key, GameObject messageParam)
    {
        if (GameObjectDictionary.ContainsKey(key))
        {
            GameObjectDictionary[key].MessageParam = messageParam;
            foreach (Action<GameObject> callback in GameObjectDictionary[key].Readers)
            {
                callback(messageParam);
            }
        }
    }

    public static void AwaitMessageGamobject(string key, Action<GameObject> action)
    {
        if (!GameObjectDictionary.ContainsKey(key))
        {
            ReadingList<GameObject> newlist = new ReadingList<GameObject>(null);
            GameObjectDictionary.Add(key, newlist);
        }
        GameObjectDictionary[key].Readers.Add(action);
    }

    public static GameObject GetMessageGameobject(string key)
    {
        if (GameObjectDictionary.ContainsKey(key))
            return GameObjectDictionary[key].MessageParam;
        else
            return null;
    }


}
