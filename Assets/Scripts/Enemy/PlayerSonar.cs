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
            Vector3 target = new Vector3(other.transform.position.x, thisEnemy.transform.position.y, other.transform.position.z);
            thisEnemy.GetComponent<NavMeshAgent>().SetDestination(target);
        }
    }
}
