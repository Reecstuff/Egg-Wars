using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Trap : MonoBehaviour
{
    [SerializeField]
    protected int damage = 1;

    [SerializeField]
    protected float timeToKill = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() && !DungeonMaster.Instance.player.GravityOn)
        {
            IsAboutToDamagePlayer();
        }
    }


    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerController>() && !DungeonMaster.Instance.player.GravityOn)
        {
            CancelInvoke(nameof(DamagePlayer));
        }
    }

    protected virtual void IsAboutToDamagePlayer()
    {
        Invoke(nameof(DamagePlayer), timeToKill);
    }

    protected virtual void DamagePlayer()
    {
        DungeonMaster.Instance.player.characterStats.TakeDamage(damage);
    }
}
