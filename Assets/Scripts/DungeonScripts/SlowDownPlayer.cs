using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlowDownPlayer : MonoBehaviour
{
    [SerializeField]
    float slowDownSpeed = 2;

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
            DungeonMaster.Instance.player.OnSpeedChange(DungeonMaster.Instance.player.moveSpeed / 2);
        }

        if (other.GetComponent<Enemy>())
        {
            Enemy thisEnemy = other.GetComponent<Enemy>();
            thisEnemy.OnSpeedChange(thisEnemy.Speed / slowDownSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            DungeonMaster.Instance.player.OnSpeedChange(DungeonMaster.Instance.player.standardSpeed);
        }

        if(other.GetComponent<Enemy>())
        {
            Enemy thisEnemy = other.GetComponent<Enemy>();
            thisEnemy.OnSpeedChange(thisEnemy.standardSpeed);
        }
    }
}
