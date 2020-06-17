using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemDefinition Definition;

    private void OnTriggerEnter(Collider other)
    {
        CharacterObject character = other.GetComponent<CharacterObject>();
        if (character)
        {
            character.Controller.AddObjectToInventory(new ItemInstance(Definition));
            Destroy(this.gameObject);
        }
    }
}
