using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticFunctions;

public class BattleInputManager : BaseInputManager
{
    private BattleSetup Setup;

    public void Init(BattleSetup setup)
    {
        Setup = setup;
    }

    private Vector3 HoverPoint;

    public override void ChangeSelectCharacter(BaseCharacter newCharacter)
    {
        SelectedPlayerCharacter = newCharacter;
    }

    protected override void HandleClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedPlayerCharacter)
            {
                RaycastHit floorHit = CheckIfObjectIsInFocus(Constants.FloorTag, FloorDetectionTarget, true);
                if (floorHit.collider != null)
                {
                    Setup.InputClick(HoverPoint, SelectedPlayerCharacter);
                }
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.S))
        {
            GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.EndBattleKey, null);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Setup.Combat_EndTurn();
        }
    }

    protected override void HandleHover()
    {
        RaycastHit floorHit = CheckIfObjectIsInFocus(Constants.FloorTag, FloorDetectionTarget, true);
        if (floorHit.collider != null)
        {
            HoverPoint = floorHit.point;
            Setup.UpdateGridHoverPosition(HoverPoint);
            Cursor.SetCursor(SuccessTexture, Vector2.zero, CursorMode.Auto);
        }
        else
            Cursor.SetCursor(FailureTexture, Vector2.zero, CursorMode.Auto);
    }

}
