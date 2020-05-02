using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    public float Speed = 10;
    public float standardSpeed;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        standardSpeed = Speed;
        OnSpeedChange(Speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSpeedChange(float newSpeed)
    {
        Speed = newSpeed;
        agent.speed = Speed;

        // Set Animation Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Destroy(gameObject); //Or takes damage
        }
    }

}
