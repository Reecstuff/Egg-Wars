using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquipAbleItem
{
    public Transform firePoint;
    public GameObject bullet;

    public float spreadAngle = 10;
    public float shootSpeed = 10;
    public float bulletcount = 1;

    public int damage = 1;

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
                    Shoot();
                    break;
                case "EggPistole":
                    Shoot();
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
                case "Sense":
                    Swing();
                    break;
                case "Heugabel":
                    Stab();
                    break;
                case "RiotShield":
                    Stab();
                    break;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void Stab()
    {
        GetComponentInParent<PlayerController>().StabWeapon();

        GetComponentInChildren<MeleeCombat>().damage = damage;
    }

    void Swing()
    {
        GetComponentInParent<PlayerController>().SwingWeapon();
        
        GetComponentInChildren<MeleeCombat>().damage = damage;
    }

    public void Shoot()
    {
        int i = 0;

        while (i < bulletcount)
        {
            GameObject p = Instantiate(bullet, firePoint.position, firePoint.rotation);

            if (item.name == "EggShotgun")
            {
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, Random.rotation, spreadAngle);
            }

            if (item.name == "Strohballenwerfer")
            {
                p.GetComponent<Heuballen>().damage = damage;
            }
            else
            {
                p.GetComponent<Bullet>().damage = damage;
            }

            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * shootSpeed, ForceMode.Impulse);

            i++;
        }
    }
}
