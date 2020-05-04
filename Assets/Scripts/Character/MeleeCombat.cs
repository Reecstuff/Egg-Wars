using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public int damage = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.GotHit(damage);

            if (enemy.health <= 0)
                enemy.EnemyDying();
        }
    }
}