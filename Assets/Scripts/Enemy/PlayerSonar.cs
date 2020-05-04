using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class PlayerSonar : MonoBehaviour
{
    Enemy thisEnemy;



    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        thisEnemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            thisEnemy.PlayerEnterTrigger(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            thisEnemy.PlayerInTriggerStay(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            thisEnemy.PlayerExitTrigger(other.gameObject);
        }
    }
}
