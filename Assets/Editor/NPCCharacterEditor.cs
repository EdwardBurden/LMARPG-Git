using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PlayerCharcater), true)]
public class NPCCharacterEditor : SerialisedGameObjectEditor<SD_PlayerCharacter>
{
    public override void OpenNew()
    {
        CreateNewDataWindow.OpenWindow<SD_PlayerCharacter>();
    }
}

