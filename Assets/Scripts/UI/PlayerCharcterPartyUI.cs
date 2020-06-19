using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharcterPartyUI : MonoBehaviour
{
    [SerializeField] private Image IconImage;
    [SerializeField] private Text NameText;
    public Button SelectButton;

    public void Init(PlayerCharacterDefinition playerCharacterDefinition)
    {
        IconImage.sprite = playerCharacterDefinition.Icon;
        NameText.text = playerCharacterDefinition.Name;
    }
}
