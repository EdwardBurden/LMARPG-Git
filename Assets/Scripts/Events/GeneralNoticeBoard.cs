using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GeneralNoticeBoard
{
    public const string StartBattleKey = "StartBattle";
    public const string EndBattleKey = "EndBattle";
    public const string AddNPCToBattleKey = "AddToBattle";
    private static Dictionary<string, ReadingList<GameObject>> GameObjectDictionary = new Dictionary<string, ReadingList<GameObject>>();

    private class Reader<T>
    {
        public MonoBehaviour Reciever;
        public Action<T> Action;
    }


    private class ReadingList<T>
    {
        public MonoBehaviour Reciever;
        public T MessageParam;
        public List<Reader<T>> Readers;

        public ReadingList(T param)
        {
            MessageParam = param;
            Readers = new List<Reader<T>>();
        }
    }


    public static void PostMessageGameobject(string key, GameObject messageParam)
    {
        if (GameObjectDictionary.ContainsKey(key))
        {
            GameObjectDictionary[key].MessageParam = messageParam;
            foreach (var reader in GameObjectDictionary[key].Readers)
            {
                reader.Action(messageParam);
            }
        }
    }

    public static void AwaitMessageGamobject(string key, Action<GameObject> action, MonoBehaviour reader)
    {
        if (!GameObjectDictionary.ContainsKey(key))
        {
            ReadingList<GameObject> newlist = new ReadingList<GameObject>(null);
            GameObjectDictionary.Add(key, newlist);
        }
        GameObjectDictionary[key].Readers.Add(new Reader<GameObject>() { Reciever = reader, Action = action });
    }

    public static GameObject GetMessageGameobject(string key)
    {
        if (GameObjectDictionary.ContainsKey(key))
            return GameObjectDictionary[key].MessageParam;
        else
            return null;
    }

    public static void RemoveAwaitGameobject(string key , MonoBehaviour reader)
    {
        if (GameObjectDictionary.ContainsKey(key))
        {
            GameObjectDictionary[key].Readers.RemoveAll(x => x.Reciever == reader);
        }
    }
}
