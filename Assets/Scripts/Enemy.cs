using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float Speed = 10;
    public float standardSpeed;


    // Start is called before the first frame update
    void Start()
    {
        standardSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSpeedChange(float newSpeed)
    {
        Speed = newSpeed;
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
