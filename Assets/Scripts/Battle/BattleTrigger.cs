using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleTrigger : MonoBehaviour
{
    public float EffectRadius;
    [HideInInspector]
    public float BattleRadius;
    public float AggressionLevel = 5.0f;
    public List<NPCCharacter> InludedCharacters;

    private void Awake()
    {
        BattleRadius = GetComponent<SphereCollider>().radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharcater>())
        {
            GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.StartBattleKey, this.gameObject);
        }

        if (other.gameObject.GetComponent<NPCCharacter>())
        {
            GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.AddNPCToBattleKey, other.gameObject);
        }
    }
}
