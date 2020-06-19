using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticFunctions;

public class RoamInputManager : BaseInputManager
{
    private RoamSetup Setup;

    public void Init(RoamSetup setup)
    {
        Setup = setup;
    }
    public Action<PlayerCharcater,Vector3> MoveCharcater;

    protected override void HandleClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedPlayerCharacter)
            {
                RaycastHit floorHit = CheckIfObjectIsInFocus("Floor");
                if (floorHit.collider != null)
                {
                    MoveCharcater(SelectedPlayerCharacter, floorHit.point);
                }
            }
        }
    }

    protected override void HandleHover()
    {
        if (SelectedPlayerCharacter)
        {
            RaycastHit floorHit = CheckIfObjectIsInFocus("Floor");
            if (floorHit.collider != null)
            {
                Cursor.SetCursor(SuccessTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(FailureTexture, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public override void ChangeSelectCharacter(PlayerCharcater character)
    {
        SelectedPlayerCharacter = character;
    }

}
