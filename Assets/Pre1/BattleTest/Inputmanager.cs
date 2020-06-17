using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputmanager : MonoBehaviour
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

    public BattleManager BattleManager;
    private BattleCharacter SelectedCharacter;

    void Update()
    {
        HandleClick();
    }

    private void HandleClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RayCastInfo<BattleCharacter> hit = CheckIfObjectIsInFocus<BattleCharacter>();
            if (hit.Focused && hit.FocusObject != SelectedCharacter)
            {
                SelectedCharacter = hit.FocusObject;
            }
            else
            {
                RaycastHit taghit = CheckIfObjectIsInFocus("Floor");
                if (SelectedCharacter)
                {
                    BattleManager.HandleMovement(SelectedCharacter, taghit.point);
                }
            }
        }
    }

    public static RaycastHit CheckIfObjectIsInFocus(string tag)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 10000;
        RaycastHit[] raycasts = Physics.RaycastAll(ray);
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].distance < distance)
            {
                distance = raycasts[i].distance;
                bool found = raycasts[i].collider.gameObject.CompareTag(tag);
                if (found)
                {
                    hit = raycasts[i];
                }

            }
        }
        return hit;
    }

    public static RayCastInfo<T> CheckIfObjectIsInFocus<T>() where T : MonoBehaviour
    {
        RaycastHit hit = new RaycastHit();
        bool inSight = false;
        T foundObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 10000;
        RaycastHit[] raycasts = Physics.RaycastAll(ray);
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].distance < distance)
            {
                distance = raycasts[i].distance;
                T foundobject = raycasts[i].collider.gameObject.GetComponent<T>();
                if (foundobject)
                {
                    inSight = true;
                    hit = raycasts[i];
                    foundObject = foundobject;
                }
                else
                {
                    inSight = false;
                }

            }
        }
        return new RayCastInfo<T>(inSight, hit, foundObject);
    }
}
