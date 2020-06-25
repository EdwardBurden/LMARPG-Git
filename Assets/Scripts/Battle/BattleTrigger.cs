using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharcater>())
        {
            GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.BattleTriggerKey, this.gameObject);
            GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}
