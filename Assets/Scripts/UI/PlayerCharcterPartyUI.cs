using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharcterPartyUI : MonoBehaviour
{
    [SerializeField] private Image IconImage;
    [SerializeField] private Text NameText;
    public Button SelectButton;
    public Color HighlightColour;
    public Color DefaultColour;

    public void Init(CharacterDefinition playerCharacterDefinition)
    {
        IconImage.sprite = playerCharacterDefinition.Icon;
        NameText.text = playerCharacterDefinition.Name;
        HighlightOff();
    }

    public void HighlightOff()
    {
        SelectButton.targetGraphic.color = DefaultColour;
    }

    public void HighlightOn()
    {
        SelectButton.targetGraphic.color = HighlightColour;
    }
}
