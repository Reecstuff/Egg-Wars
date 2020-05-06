using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAbleItem : MonoBehaviour
{
    public ItemText item;

    [Range(0, 2)]
    public float moveSpeedMultiplier = 1;
}
