using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Button DeleteButton;
    [SerializeField] private Button DropButton;
    [SerializeField] private Image Icon;
    [SerializeField] private Text Name;
    [SerializeField] private Text Weight;

    private ItemInstance ItemData;

    public Action<ItemInstance> DeleteClicked;
    public Action<ItemInstance> DropClicked;

    public void Init(ItemInstance instance)
    {
        ItemData = instance;

        Icon.sprite = instance.ItemDefinition.Icon;
        Name.text = instance.ItemDefinition.Name;
        Weight.text = instance.ItemDefinition.Weight.ToString();

        DeleteButton.onClick.RemoveAllListeners();
        DropButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.AddListener(() => DeleteClicked(ItemData));
        DropButton.onClick.AddListener(() => DropClicked(ItemData));
    }
}
