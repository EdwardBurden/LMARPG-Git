using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyData")]
public class PartyTemplateData : ScriptableObject
{
    public string PartyName;
    public List<CharacterTemplateData> Characters;
}
