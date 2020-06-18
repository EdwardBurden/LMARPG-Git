using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private TransformUnityEvent OnTriggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharcater>())
        {
            OnTriggerEvent.Invoke(this.transform);
        }
    }
}
