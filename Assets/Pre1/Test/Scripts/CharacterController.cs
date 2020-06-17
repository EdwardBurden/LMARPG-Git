using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController
{
    private CharacterInventory _Inventory;
    public CharacterInventory Inventory { get { return _Inventory; } }

    private CharacterQuestLog _QuestLog;
    public CharacterQuestLog QuestLog { get { return _QuestLog; } }

    private CharacterInformation _Information;
    public CharacterInformation Information { get { return _Information; } }

    private CharacterObject _CObject; 
    public CharacterObject CObject { get { return _CObject; } }

    public CharacterObject Prefab;
    public Action ItemAdded;

    public void AddObjectToInventory(ItemInstance item)
    {
        _Inventory.AddObject(item);
        ItemAdded(); //only needed if this is the selected character
    }

    public CharacterController(CharacterTemplateData templateData) //From Scriptable
    {
        _Inventory = new CharacterInventory(templateData);
        _QuestLog = new CharacterQuestLog();
        _Information = new CharacterInformation(templateData.CharacterName, templateData.Speed);
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

    public CharacterController(CharacterData saveData) //From File
    {
        _Inventory = new CharacterInventory(saveData);
        _QuestLog = new CharacterQuestLog();
        _Information = new CharacterInformation(saveData.Name, saveData.Speed);
        GameObject obj = Resources.Load(saveData.PrefabString, typeof(GameObject)) as GameObject;
        Prefab = obj.GetComponent<CharacterObject>();
    }

    public void AssignObject(CharacterObject charObj)
    {
        _CObject = charObj;
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
