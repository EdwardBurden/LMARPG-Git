using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] BattleGrid BattleGrid;

    public bool Mode;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.T)) {
            SwitchMode();
        }
    }



    public void SwitchMode()
    {
        Mode = !Mode;
        if (Mode)
        {
            BattleGrid.Activate();
            BattleGrid.gameObject.SetActive(true);
        }
        else
            BattleGrid.gameObject.SetActive(false);
    }

    public void HandleMovement(BattleCharacter character, Vector3 targte)
    {
        if (Mode)
        {
            //grid
        }
        else
        {
            character.MoveTo(targte);
        }


    }


}
