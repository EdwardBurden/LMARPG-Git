using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    private List<CharacterController> Characters;
    private int SelectedCharacter;

    //todo move into somewhere else appropriate
    [SerializeField]
    private PartyTemplateData StartingData = null;



    private void Start()
    {
        Characters = new List<CharacterController>();
        if (StartingData != null)
            SpawnCharactersFromTemplate();
    }

    private void SetDefaultSelectedCharacter(PartyData partyData)
    {
        SelectedCharacter = partyData.LastSelectedCharacter;
        Characters[SelectedCharacter].CharacterSelected();
    }



    public void LoadParty(PartyData partyData)
    {
        foreach (CharacterController character in Characters) //would be handled in sceneunload
        {
            character.Destroy();
        }
        Characters = new List<CharacterController>();
        SpawnCharactersFromSaveData(partyData);
        SetDefaultSelectedCharacter(partyData);
    }

    public void CharacterSelected(CharacterController characterController)
    {
        SelectedCharacter = Characters.IndexOf(characterController);
        Characters[SelectedCharacter].CharacterSelected();
    }

    public PartyData ExportToSaveData()
    {
        PartyData pdata = new PartyData();
        List<CharacterData> characterdatas = new List<CharacterData>();
        for (int i = 0; i < Characters.Count; i++)
        {
            pdata.LastSelectedCharacter = SelectedCharacter;
            CharacterData CData = Characters[i].ExportToSaveData();
            characterdatas.Add(CData);

        }
        pdata.Characters = characterdatas;
        return pdata;
    }

    private void SpawnCharactersFromSaveData(PartyData partyData)
    {
        foreach (CharacterData charData in partyData.Characters)
        {
            CharacterController newCharController = new CharacterController(charData);
            CharacterObject charObj = Instantiate(newCharController.Prefab, this.transform);
            charObj.Init(newCharController);
            charObj.OnClick += CharacterSelected;
            newCharController.AssignObject(charObj);
            Characters.Add(newCharController);
            charObj.transform.position = charData.Position;
        }

    }

    private void SpawnCharactersFromTemplate()
    {
        foreach (CharacterTemplateData charData in StartingData.Characters)
        {
            CharacterController newCharController = new CharacterController(charData);
            CharacterObject charObj = Instantiate(charData.Prefab, this.transform);
            charObj.Init(newCharController);
            charObj.OnClick += CharacterSelected;
            newCharController.AssignObject(charObj);
            Characters.Add(newCharController);
        }

    }
}
