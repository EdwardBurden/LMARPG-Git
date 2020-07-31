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
        CreateNewDataWindow.OpenWindow<SD_PlayerCharacter>();
    }
}

