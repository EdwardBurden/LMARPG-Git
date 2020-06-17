using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaseInputManager ActiveInput;
    [SerializeField] private BaseInputManager RoamSetup;
    [SerializeField] private BaseInputManager BattleSetup;
    [SerializeField] private PlayerCharcater ActivePlayerCharacter;
    [SerializeField] private List<PlayerCharcater> PlayerCharacters;

    public void Start()
    {
        ActiveInput.Init(this);
        ActiveInput.ChangeSelectCharacter(ActivePlayerCharacter);

    }

    public void SwitchToBattleSetup()
    {
        Destroy(ActiveInput.gameObject);
        ActiveInput = Instantiate(BattleSetup);
        ActiveInput.gameObject.GetComponent<BattleGrid>().Activate(PlayerCharacters);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            SwitchToRoamSetup();

        }
    }

    public void SwitchToRoamSetup()
    {
        Destroy(ActiveInput.gameObject);
        ActiveInput = Instantiate(RoamSetup);
        ActiveInput.Init(this);
        ActiveInput.ChangeSelectCharacter(ActivePlayerCharacter);
    }
}
