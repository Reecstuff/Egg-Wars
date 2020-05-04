using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Enemy
{

    protected override void OnPlayerCollision(GameObject player)
    {
        base.OnPlayerCollision(player);
        EnemyDying();
    }

    public override void EnemyDying()
    {
        base.EnemyDying();
        sonar.gameObject.SetActive(false);
        transform.DOScale(1.5f, 0.6f);
        Invoke(nameof(DamagePlayer), 0.6f);
        maxPitch = 0;
        source.Stop();
        source.Play();
        source.DOPitch(3, 0.1f);
        source.DOFade(2, 0.5f);
        Invoke(nameof(DoParticle), 0.5f);
        Invoke(nameof(Die), 1f);
    }

    void DoParticle()
    {
        animator.gameObject.SetActive(false);
        PlayParticles();
    }

    public override void DamagePlayer()
    {
        float radius = 7f;
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

    public override void PlayerEnterTrigger(GameObject other)
    {
        base.PlayerEnterTrigger(other);
        Vector3 target = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
       
        if(agent.isActiveAndEnabled && agent.isOnNavMesh)
            agent.SetDestination(target);
        PlayAudio();
    }

    public override void PlayerInTriggerStay(GameObject other)
    {
        base.PlayerInTriggerStay(other);
        Vector3 target = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
        if (wiggleSafe <= 0)
        {
            agent.SetDestination(target);
            wiggleSafe = 5;
        }
        PitchAudio(Vector3.Distance(target, transform.position) * 0.05f);
        PlayAudio();
        wiggleSafe--;
    }

    public override void SomethingElseIsInTrigger()
    {
        base.SomethingElseIsInTrigger();
    }
}
