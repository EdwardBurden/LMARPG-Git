using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName  = "ScriptableObject/Race")]
public class RaceDefinition : ScriptableObject
{
    public string RaceName;
    public int MovementSpeed;
    public AbilityScoreTypes AbilityBonusType;
    public int AbilityBonusAmount;
}
