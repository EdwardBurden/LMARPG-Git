using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PartyController PartyController;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            SaveGame();
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            LoadGame();
        }
    }
    public void SaveGame()
    {
        GameSaveData saveData = new GameSaveData(PartyController);
        GameSaveDataManager.SaveGame(saveData, "ed");
    }

    public void LoadGame()
    {
        GameSaveData saveData = GameSaveDataManager.LoadGame("ed");
        PartyController.LoadParty(saveData.Party);
    }
}
