using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class StaticFunctions
{
    public struct RayCastInfo<T> where T : MonoBehaviour
    {
        public bool Focused;
        public T FocusObject;
        public RaycastHit HitInfo;
        public RayCastInfo(bool focus, RaycastHit info, T focusobject)
        {
            Focused = focus;
            HitInfo = info;
            FocusObject = focusobject;
        }
    }

    public static RaycastHit CheckIfObjectIsInFocus(string tag, bool allowBlock)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycasts = Physics.RaycastAll(ray);
        raycasts = raycasts.Where(x => allowBlock || x.collider.gameObject.CompareTag(tag)).OrderBy(x => x.distance).ToArray();

        if (raycasts.Length > 0 && raycasts[0].collider.gameObject.CompareTag(tag))
        {
            hit = raycasts[0];
        }
        return hit;
    }

    public static RayCastInfo<T> CheckIfObjectIsInFocus<T>(bool allowBlock) where T : MonoBehaviour
    {
        RaycastHit hit = new RaycastHit();
        bool inSight = false;
        T foundObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycasts = Physics.RaycastAll(ray);

        raycasts = raycasts.Where(x => allowBlock || x.collider.gameObject.GetComponent<T>()).OrderBy(x => x.distance).ToArray();
        if (raycasts.Length > 0 && raycasts[0].collider.gameObject.GetComponent<T>())
        {
            inSight = true;
            hit = raycasts[0];
            foundObject = raycasts[0].collider.gameObject.GetComponent<T>();
        }
        else
        {
            inSight = false;
        }

        return new RayCastInfo<T>(inSight, hit, foundObject);
    }

    public static List<Type> GetDerivedTypes(Type providedtype)
    {
        return Assembly.GetAssembly(providedtype).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && (myType.IsSubclassOf(providedtype) || myType == providedtype)).ToList();
    }

}
