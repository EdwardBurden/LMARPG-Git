using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class Utils
{
    public static List<Type> GetDerivedTypes(Type providedtype)
    {
        return Assembly.GetAssembly(providedtype).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && (myType.IsSubclassOf(providedtype) || myType == providedtype)).ToList();
    }

}
