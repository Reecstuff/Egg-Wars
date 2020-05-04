using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : Trap
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !DungeonMaster.Instance.player.GravityOn)
        {
            DamagePlayer();
        }
    }
}
