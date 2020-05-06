using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : Trap
{
    [SerializeField]
    ParticleSystem[] particleSystem;

    protected override void IsAboutToDamagePlayer()
    {
        for (int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].Play();
        }
        base.IsAboutToDamagePlayer();
        GetComponent<Collider>().enabled = false;
        Invoke(nameof(DestroyTrap), timeToKill + 0.01f);
    }


    void DestroyTrap()
    {
        Destroy(gameObject);
    }
}
