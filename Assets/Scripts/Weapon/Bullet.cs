using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timeBtwShots = 2;
    float startTimeBtwShots = 0;

    public int damage;

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
        if (other.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            enemy.health -= damage;

            if(enemy.health <= 0)
                enemy.EnemyDying();
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
