using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquipAbleItem
{
    public Transform firePoint;
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


            switch(item.name)
            {
                case "EggShotgun":
                    Shotgun();
                    break;
                case "EggGewehr":
                    Shoot();
                    break;
                case "Strohballenwerfer":
                    Shoot();
                    break;
                case "Milkgun":
                    Shoot();
                    break;
                case "LaserKarottenSchwert":
                    Swing();
                    GetComponent<LaserAnimation>().ShowLaser();
                    break;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void Swing()
    {
        GetComponentInParent<PlayerController>().SwingWeapon();
    }

    public void Shoot()
    {
        GameObject p = Instantiate(bullet, firePoint.position, firePoint.rotation);
        p.GetComponent<Rigidbody>().AddForce(p.transform.forward * shootSpeed, ForceMode.Impulse);
    }

    public void Shotgun()
    {
        int i = 0;

        while (i < bulletcount)
        {
            GameObject p = Instantiate(bullet, firePoint.position, firePoint.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, Random.rotation, spreadAngle);
            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * shootSpeed, ForceMode.Impulse);

            i++;
        }
    }
}
