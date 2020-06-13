using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory
{
    private List<ItemInstance> Items = new List<ItemInstance>();

    public CharacterInventory(CharacterData characterData)
    {
    }

    public CharacterInventory(CharacterTemplateData characterData)
    {


    }

    public void AddObject(ItemInstance item)
    {
        Items.Add(item);
    }

    public void RemoveObject(ItemInstance item)
    {
        Items.Remove(item);
    }

    public List<ItemInstance> GetDisplayItems()
    {
        return Items; //can modify later with filter
    }

}
