using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableObject), true)]
public class HealthItemEditor : SerialisedGameObjectEditor<SD_HealthItem>
{
    public override void OpenNew()
    {
        CreateNewDataWindow.OpenNewWindow<SD_HealthItem>((InteractableObject)target);
    }

    public override void OpenView(SD_HealthItem data)
    {
        ViewDataWindow.OpenViewWindow<SD_HealthItem>((InteractableObject)target, data);
    }
}
