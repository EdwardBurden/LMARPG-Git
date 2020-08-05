using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticFunctions;

public class RoamInputManager : BaseInputManager
{
    [SerializeField] protected Texture2D InteractTexure;
    private RoamSetup Setup;

    public void Init(RoamSetup setup)
    {
        Setup = setup;
    }

    public Action<BaseCharacter, Vector3> MoveCharcater;
    public Action<BaseCharacter, InteractableObject> MoveToInteract;

    protected override void HandleClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedPlayerCharacter)
            {
                RayCastInfo<InteractableObject> HoverObject = CheckIfObjectIsInFocus<InteractableObject>(true);
                if (HoverObject.Focused)
                {
                    MoveToInteract(SelectedPlayerCharacter, HoverObject.FocusObject);
                }
                else
                {
                    RaycastHit floorHit = CheckIfObjectIsInFocus("Floor", true);
                    if (floorHit.collider != null)
                    {
                        MoveCharcater(SelectedPlayerCharacter, floorHit.point);
                    }
                }
            }
        }
    }

    protected override void HandleHover()
    {
        if (SelectedPlayerCharacter)
        {
            RayCastInfo<InteractableObject> HoverObject = CheckIfObjectIsInFocus<InteractableObject>(true);
            if (HoverObject.Focused)
            {
                Cursor.SetCursor(InteractTexure, Vector2.zero, CursorMode.Auto);

            }
            else
            {
                RaycastHit floorHit = CheckIfObjectIsInFocus("Floor", true);
                if (floorHit.collider != null)
                {
                    Cursor.SetCursor(SuccessTexture, Vector2.zero, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(FailureTexture, Vector2.zero, CursorMode.Auto);
                }
            }

        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public override void ChangeSelectCharacter(BaseCharacter character)
    {
        SelectedPlayerCharacter = character;
    }

}
