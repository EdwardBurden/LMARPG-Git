using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private Transform Content;

    [SerializeField] private ItemDisplay ItemUI;

    private CharacterController SelectedCharcater;

    public void SetCharacter(CharacterController character)
    {
        SelectedCharcater = character;
    }

    public void Refresh()
    {
        foreach (Transform item in Content)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in SelectedCharcater.Inventory.GetDisplayItems())
        {
            ItemDisplay display = Instantiate(ItemUI, Content);
            display.Init(item);
            display.DeleteClicked += Delete;
            display.DropClicked += Drop;
        }
    }

    public void Delete(ItemInstance itemInstance)
    {
        SelectedCharcater.Inventory.RemoveObject(itemInstance);
        Refresh();
    }

    public void Drop(ItemInstance itemInstance)
    {
        Refresh();

    }
}
