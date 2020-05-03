﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Enemy
{
    protected override void OnPlayerCollision(GameObject player)
    {
        base.OnPlayerCollision(player);
        sonar.gameObject.SetActive(false);
        transform.DOScale(1.5f, 0.6f);
        Invoke(nameof(DamagePlayer), 0.6f);
        maxPitch = 0;
        source.Stop();
        source.Play();
        source.DOPitch(3, 0.1f);
        source.DOFade(2, 0.5f);
        Invoke(nameof(PlayParticles), 0.5f);
        Invoke(nameof(Die), 1f);
    }

    protected override void DamagePlayer()
    {
        float radius = 6f;
        float force = 2000f;

        Collider[] collider = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObj in collider)
        {
            Rigidbody rb = nearbyObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            CharacterStats destruct = nearbyObj.GetComponent<CharacterStats>();
            if (destruct != null)
            {
                destruct.TakeDamage(damage);
            }
        }
    }
}
