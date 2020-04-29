using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public Camera cam;

    Vector3 moveInput;
    Vector3 moveVelocity;

    public float moveSpeed = 20f;

    public GameObject[] weapons = new GameObject[2];
    public GameObject equippedWeapon;
    public Transform weaponSlot;
    public int granates;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "collectable" && Input.GetKey(KeyCode.F))
        {
            Destroy(equippedWeapon);

            if (other.gameObject.name == "EggPistol")
            {
                equippedWeapon = Instantiate(weapons[0], weaponSlot.position, weaponSlot.rotation) as GameObject;
            }
            if (other.gameObject.name == "EggRifle")
            {
                equippedWeapon = Instantiate(weapons[1], weaponSlot.position, weaponSlot.rotation) as GameObject;
            }

            equippedWeapon.transform.parent = GameObject.Find("Player").transform;

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Granate")
        {
            granates++;

            Destroy(other.gameObject);
        }
    }
}
