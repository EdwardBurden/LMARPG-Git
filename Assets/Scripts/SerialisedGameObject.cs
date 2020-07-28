using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SerialisedGameObject : MonoBehaviour
{
    public abstract SD_Base BaseData { get; set; }
}
