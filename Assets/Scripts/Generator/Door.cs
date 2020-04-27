using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour
{
    public Door nextDoor;
    bool doorVisited;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        doorVisited = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check for player
        if(other.gameObject.GetComponent<PlayerController>())
        {
            // Check if generate Dungeon
            if(!doorVisited)
            {
                // Generate Dungeons in next Dungeon
            }
            else
            {

            }
        }
    }


    void MovePosition()
    {

    }
}
