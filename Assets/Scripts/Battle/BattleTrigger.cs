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

    public bool BattleStarted;

    private void Awake()
    {
        BattleRadius = GetComponent<SphereCollider>().radius;
        BattleStarted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool battlestart = false;
        if (!BattleStarted && other.gameObject.GetComponent<PlayerCharcater>())
        {
            PlayerCollided();
            battlestart = true;
        }

        if (other.gameObject.GetComponent<NPCCharacter>())
        {
            CharacterColidded(other.gameObject);
        }

        if (battlestart && !BattleStarted)
        {
            ActivateIncludedCharactersForBattle();
            BattleStarted = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (BattleStarted)
        {

            if (other.gameObject.GetComponent<NPCCharacter>())
            {
                CharacterColidded(other.gameObject);
            }

        }
    }

    private void PlayerCollided()
    {
        GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.StartBattleKey, this.gameObject);
    }

    private void CharacterColidded(GameObject other)
    {
        GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.AddNPCToBattleKey, other.gameObject);
    }

    private void ActivateIncludedCharactersForBattle()
    {
        foreach (var item in InludedCharacters)
        {
            GeneralNoticeBoard.PostMessageGameobject(GeneralNoticeBoard.AddNPCToBattleKey, item.gameObject);
        }
    }
}
