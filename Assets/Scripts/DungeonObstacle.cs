using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DungeonObstacle
{
    [Range(0, 100)]
    public int SpawnPercentage;
    public GameObject prefab;
}
