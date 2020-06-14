using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<BattleCharacter> Characters = new List<BattleCharacter>();
    [SerializeField] BattleGrid BattleGrid;

    private void Update()
    {
        BattleGrid.AlignObjects(Characters);
    }
}
