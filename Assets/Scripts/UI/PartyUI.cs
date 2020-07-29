using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyUI : MonoBehaviour
{
    [SerializeField] private Transform CharcaterContent;
    [SerializeField] private PlayerCharcterPartyUI CharcaterButtonPrefab;
    private List<PlayerCharcater> Characters;
    RoamSetup Setup;


    public void Init(List<PlayerCharcater> characters, RoamSetup setup)
    {
        Characters = characters;
        Setup = setup;
        foreach (var character in Characters)
        {
            PlayerCharcterPartyUI b = Instantiate(CharcaterButtonPrefab, CharcaterContent);
            b.Init(character.GetCharacterData);
            b.SelectButton.onClick.AddListener(() => OnCharcterButtonClick(character));
        }
    }

    public void OnCharcterButtonClick(PlayerCharcater pChar)
    {
        Setup.ChangeCharacter(pChar);
    }
}
