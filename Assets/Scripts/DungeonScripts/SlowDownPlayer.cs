using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlowDownPlayer : MonoBehaviour
{
    [SerializeField]
    float slowDownSpeed = 2;

    bool shouldReset = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        slowDownSpeed += DungeonMaster.Instance.levelCount / 10; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            if(!DungeonMaster.Instance.player.GravityOn)
            {
                DungeonMaster.Instance.player.OnSpeedChange(DungeonMaster.Instance.player.moveSpeed * 0.5f);
                DungeonMaster.Instance.player.PitchWalking(0.5f);
                shouldReset = true;
            }
        }

        if (other.GetComponent<Enemy>())
        {
            Enemy thisEnemy = other.GetComponent<Enemy>();
            thisEnemy.OnSpeedChange(thisEnemy.Speed * 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if(shouldReset)
            {
                DungeonMaster.Instance.player.OnSpeedChange(DungeonMaster.Instance.player.equippedWeapon.GetComponent<Weapon>().moveSpeed);
                DungeonMaster.Instance.player.PitchWalking(1);
            }
        }

        if(other.GetComponent<Enemy>())
        {
            Enemy thisEnemy = other.GetComponent<Enemy>();
            thisEnemy.OnSpeedChange(thisEnemy.standardSpeed);
        }
    }
}
