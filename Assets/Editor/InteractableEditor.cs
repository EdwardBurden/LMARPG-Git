using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableObject), true)]
public class InteractableEditor : SerialisedGameObjectEditor<SD_Item>
{
    List<Type> DerivedTypes = new List<Type>();
    int SelectedClass;

    public override void OpenNew()
    {
        CreateNewDataWindow.OpenNewWindow((InteractableObject)target, typeof(SD_Item));
    }

    public override void OpenView(SD_Item data)
    {
        ViewDataWindow.OpenViewWindow((InteractableObject)target, data);
    }
}
