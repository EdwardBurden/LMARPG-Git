using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public PartyData Party;

    public GameSaveData(PartyController partyController)
    {
        Party = partyController.ExportToSaveData();
    }
}
