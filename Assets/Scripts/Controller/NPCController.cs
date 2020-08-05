using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [HideInInspector]
    public List<NPCCharacter> NPCCharacters;

    private void Start()
    {
        NPCCharacters = FindObjectsOfType<NPCCharacter>().ToList();
    }
}
