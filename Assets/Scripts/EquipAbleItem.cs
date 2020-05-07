using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAbleItem : MonoBehaviour
{
    public ItemText item;

    [Range(0, 25)]
    public float moveSpeed = 17;
}
