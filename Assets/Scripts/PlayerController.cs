using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public Camera cam;

    Vector3 moveInput;
    Vector3 moveVelocity;
    Transform firePoint;

    public float moveSpeed = 12f;

    public string equippedWeapon;
    public int granates;

    public GameObject bullet;

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

        if (Input.GetMouseButton(0))
        {
            firePoint = transform;

            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;
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

    void Shoot()
    {
        GameObject projectile = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * 20, ForceMode.Impulse);
        
    }
}
