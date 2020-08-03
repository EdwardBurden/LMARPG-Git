using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(PlayerCharcater), true)]
public class PlayerCharcaterEditor : SerialisedGameObjectEditor<SD_PlayerCharacter>
{
    public override void OpenNew()
    {
        CreateNewDataWindow.OpenNewWindow<SD_PlayerCharacter>((PlayerCharcater)target);
    }

    public override void OpenView(SD_PlayerCharacter data)
    {
        ViewDataWindow.OpenViewWindow<SD_PlayerCharacter>((PlayerCharcater)target, data);
    }
}

