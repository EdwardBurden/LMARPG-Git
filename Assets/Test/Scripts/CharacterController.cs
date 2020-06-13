using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController
{
    private CharacterInventory Inventory;
    private CharacterQuestLog QuestLog;
    private CharacterInformation Information;

    private CharacterObject CObject;

    public CharacterObject Prefab;

    public CharacterController(CharacterTemplateData templateData) //From Scriptable
    {
        Inventory = new CharacterInventory();
        QuestLog = new CharacterQuestLog();
        Information = new CharacterInformation(templateData.CharacterName, templateData.Speed);
        Prefab = templateData.Prefab;
    }

    public void CharacterSelected()
    {
        //do something to show selection
    }

    public CharacterData ExportToSaveData()
    {
        return new CharacterData(Information.Name, CObject.transform.position, Prefab.name, Information.Speed);
    }

    public CharacterController(CharacterData saveData ) //From File
    {
        Inventory = new CharacterInventory();
        QuestLog = new CharacterQuestLog();
        Information = new CharacterInformation(saveData.Name, saveData.Speed);
        GameObject obj = Resources.Load(saveData.PrefabString, typeof(GameObject)) as GameObject;
        Prefab = obj.GetComponent<CharacterObject>();
    }

    public void AssignObject(CharacterObject charObj)
    {
        CObject = charObj;
    }

    public float GetCharacterSpeed()
    {
        return Information.Speed;
    }

    public void Destroy()
    {
        GameObject.Destroy(CObject.gameObject);
    }
}
