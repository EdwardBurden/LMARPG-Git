using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SerialisedGameObject : MonoBehaviour
{
    [HideInInspector] [SerializeField] public string DataReferanceID;
    [HideInInspector] public SD_Base ConstantData;
    public void InitialiseData<T>() where T : SD_Base
    {
        ConstantData = DataHandler.SearchAndImportByName<T>(DataReferanceID);
    }
}
