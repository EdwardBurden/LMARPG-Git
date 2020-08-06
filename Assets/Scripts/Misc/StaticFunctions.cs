using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public static bool IsPointerOverGameObject()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> UiHits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, UiHits);
        return UiHits.Count > 0;

    }

    public static RaycastHit CheckIfObjectIsInFocus(string tag, LayerMask layer, bool allowBlock)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycasts = Physics.RaycastAll(ray, 1000, layer);
        raycasts = raycasts.Where(x => allowBlock || x.collider.gameObject.CompareTag(tag)).OrderBy(x => x.distance).ToArray();

        if (raycasts.Length > 0 && raycasts[0].collider.gameObject.CompareTag(tag))
        {
            hit = raycasts[0];
        }
        return hit;
    }

    public static RayCastInfo<T> CheckIfObjectIsInFocus<T>(LayerMask layer, bool allowBlock) where T : MonoBehaviour
    {
        RaycastHit hit = new RaycastHit();
        bool inSight = false;
        T foundObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycasts = Physics.RaycastAll(ray, 1000, layer);

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
