using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(NPCCharacter), true)]
public class NPCCharacterEditor : SerialisedGameObjectEditor<SD_NPCCharacter>
{
    public override void OpenNew()
    {
        CreateNewDataWindow.OpenNewWindow<SD_NPCCharacter>((NPCCharacter)target);
    }

    public override void OpenView(SD_NPCCharacter data)
    {
        ViewDataWindow.OpenViewWindow((NPCCharacter)target, data);
    }
}

