using System.Collections.Generic;
using UnityEngine;

public class TurnOrderUI : MonoBehaviour
{
    [SerializeField] private Transform CharcaterContent;
    [SerializeField] private PlayerCharcterPartyUI CharcaterButtonPrefab;
    BattleSetup Setup;
    private List<PlayerCharcterPartyUI> CharactersUI = new List<PlayerCharcterPartyUI>();

    public void Init(List<BattleOrder> orders, BattleSetup setup)
    {
        Setup = setup;
        foreach (var item in CharactersUI)
        {
            if (item)
                Destroy(item.gameObject);
        }
        CharactersUI.Clear();

        foreach (var border in orders)
        {
            PlayerCharcterPartyUI b = Instantiate(CharcaterButtonPrefab, CharcaterContent);
            b.Init(border.Character.GetCharacterData);
            b.SelectButton.interactable = false;
            CharactersUI.Add(b);
        }
    }

    public void HighlightCurrentPlayer(int position)
    {
        foreach (var characterui in CharactersUI)
        {
            characterui.HighlightOff();
        }
        CharactersUI[position].HighlightOn();
    }
}
