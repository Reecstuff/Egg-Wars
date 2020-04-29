using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Transform firePoint;
    public GameObject bullet;

    public float shootSpeed = 20;
    private float timeBtwShots;
    public float startTimeBtwShots = 0.5f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            firePoint = transform;

            Shoot();
        }
    }

    void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            GameObject projectile = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * shootSpeed, ForceMode.Impulse);

            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
