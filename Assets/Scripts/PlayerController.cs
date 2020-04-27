using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;

    public string equippedWeapon;
    public int granates;

    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Move our player to what we hit
                motor.MoveToPoint(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "collectable" && Input.GetKeyDown(KeyCode.F))
        {
            equippedWeapon = other.gameObject.name;

            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Granate")
        {
            granates++;

            Destroy(other.gameObject);
        }
    }
}
