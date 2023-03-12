using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy_movement;

public class SpawnType : MonoBehaviour
{
    public spawn type = spawn.MELEE;
    public enum spawn
    {
        MELEE,
        RANGE,
    }
}
