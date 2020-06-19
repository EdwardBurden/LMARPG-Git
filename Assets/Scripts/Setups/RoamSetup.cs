using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamSetup : BaseSetup
{
    [SerializeField] RoamInputManager Input;

    public override void Activate()
    {
        Input.ChangeSelectCharacter(Manager.ActivePlayerCharacter);
    }

    public override void Deactivate()
    {
        //   throw new System.NotImplementedException();
    }

    public override BaseInputManager GetActiveInput()
    {
        return Input;
    }
}
