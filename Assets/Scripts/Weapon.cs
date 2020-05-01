﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquipAbleItem
{
    //public Transform firePoint;
    public GameObject bullet;

    public float spreadAngle = 10;
    public float shootSpeed = 10;
    public float bulletcount = 8;

    private float timeBtwShots;
    public float startTimeBtwShots = 0.5f;

    private void Update()
    {
        if (Input.GetMouseButton(0) && timeBtwShots <= 0)
        {
            timeBtwShots = startTimeBtwShots;

            if (item.name == "EggShotgun")
            {
                Shotgun();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        GameObject p = Instantiate(bullet, transform.position, transform.rotation);
        p.GetComponent<Rigidbody>().AddForce(p.transform.forward * shootSpeed, ForceMode.Impulse);
    }

    public void Shotgun()
    {
        int i = 0;

        while (i < bulletcount)
        {
            GameObject p = Instantiate(bullet, transform.position, transform.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, Random.rotation, spreadAngle);
            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * shootSpeed, ForceMode.Impulse);

            i++;
        }
    }
}
