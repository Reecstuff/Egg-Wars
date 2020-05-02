using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timeBtwShots = 2;
    float startTimeBtwShots = 0;

    void Update()
    {
        if (timeBtwShots <= 0)
        {
            Destroy(this.gameObject);

            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            DestroyBullet();
        }
        if (other.gameObject.GetComponentInParent<DungeonGenerator>())
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
