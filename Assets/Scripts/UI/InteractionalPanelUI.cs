using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionalPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject InteractionActionUIPrefab;
    public void Init(InteractableObject interactableObject, PlayerCharcater playerCharcater)
    {
        foreach (KeyValuePair<string, Action<PlayerCharcater>> action in interactableObject.InteractionActions)
        {
            GameObject gameObject = Instantiate(InteractionActionUIPrefab, this.transform);
            gameObject.GetComponent<Button>().onClick.AddListener(() => action.Value(playerCharcater));
            gameObject.GetComponentInChildren<Text>().text = action.Key;
        }
    }
}
