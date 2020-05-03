using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    AudioClip beginningClip;

    [SerializeField]
    AudioClip walkingClip;

    [SerializeField]
    AudioClip deathClip;

    string[] animationAttack = { "Boss_Jump", "Boss_Hack_Attack", "Boss_WirbelAttacke" };
    string[] animationStandard = { "Boss_Standing", "Boss_Walking", "Boss_Dying" };
    string walkingSpeed = "Walking_Speed";

    float damageRadius = 15;
    Vector3 damagePosition;
    bool animateMoving = false;

    protected override void Start()
    {
        base.Start();
        health = 2000;
    }


    protected override void OnPlayerCollision(GameObject Player)
    {
        base.OnPlayerCollision(Player);
    }

    public override void PlayerEnterTrigger(GameObject other)
    {
        base.PlayerEnterTrigger(other);
        Vector3 target = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);

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
            PlayAudio();
            wiggleSafe = 5;
        }
        PitchAudio(Vector3.Distance(target, transform.position) * 0.05f);
        wiggleSafe--;
    }

    public override void PlayerNOTInTriggerStay()
    {
        base.PlayerNOTInTriggerStay();
        if(!animateMoving && !animator.GetCurrentAnimatorStateInfo(0).IsName(animationStandard[0]))
        {
            animator.CrossFade(animationStandard[0], 0.0f);
            PlayAudio();
        }
    }


    protected override void DamagePlayer()
    {
        float force = 2000f;

        Collider[] collider = Physics.OverlapSphere(damagePosition, damageRadius);

        foreach (Collider nearbyObj in collider)
        {
            Rigidbody rb = nearbyObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, damagePosition, damageRadius);
            }

            CharacterStats destruct = nearbyObj.GetComponent<CharacterStats>();
            if (destruct != null)
            {
                destruct.TakeDamage(damage);
            }
        }
    }
}
