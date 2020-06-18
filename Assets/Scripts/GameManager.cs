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

    public void SwitchToBattleSetup(Transform triggertransform)
    {
        Destroy(ActiveInput.gameObject);
        ActiveInput = GameObject.Instantiate(BattleSetup, triggertransform.position, triggertransform.rotation, null);
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
