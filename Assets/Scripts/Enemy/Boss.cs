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

    [SerializeField]
    float attackDistance = 4;

    string[] animationAttack = { "Boss_Jump", "Boss_Hack_Attack", "Boss_WirbelAttacke" };
    string[] animationStandard = { "Boss_Standing", "Boss_Walking", "Boss_Dying" };
    string walkingSpeed = "Walking_Speed";

    float damageRadius = 15;
    Vector3 damagePosition;


    bool stopAll = false;
    bool stopNavMesh = false;
    bool stopAnimation = false;

    float timedStoppMoving = 0;
    float timeDamagDealing = 0;
    float standardY;

    protected override void Start()
    {
        base.Start();
        animator.SetFloat(walkingSpeed, agent.speed / 2);
        standardY = transform.localPosition.y;
    }


    protected override void OnPlayerCollision(GameObject Player)
    {
        base.OnPlayerCollision(Player);
    }

    public override void PlayerEnterTrigger(GameObject other)
    {
        if(!stopAll)
        {
            base.PlayerEnterTrigger(other);
            Vector3 target = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);

            agent.SetDestination(target);
            PlayAudio(beginningClip);
        }
    }

    public override void PlayerInTriggerStay(GameObject other)
    {
        if(!stopAll)
        {
            base.PlayerInTriggerStay(other);
            Vector3 target = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            float distance = Vector3.Distance(target, transform.position);
            if (wiggleSafe <= 0)
            {
                Debug.Log(target);
                agent.SetDestination(target);
                wiggleSafe = 5;
            }
            if(!stopAnimation && !animator.GetCurrentAnimatorStateInfo(0).IsName(animationStandard[1]) && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                animator.Play(animationStandard[1]);
            }
            wiggleSafe--;

            if(source.clip != walkingClip)
                PlayAudio(walkingClip, true);

            if(distance <= attackDistance)
            {
                AnimateBoss();
            }
        }
    }

    public override void PlayerExitTrigger(GameObject other)
    {
        if (!stopAll && !animator.GetCurrentAnimatorStateInfo(0).IsName(animationStandard[0]))
        {
            animator.CrossFade(animationStandard[0], 0.0f);
            PlayAudio(beginningClip);
        }
    }


    public override void DamagePlayer()
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

    public override void EnemyDying()
    {
        base.EnemyDying();
        stopAll = true;
        animator.Play(animationStandard[2]);
        Invoke(nameof(Die), 4);
    }

    void AnimateBoss()
    {
        // Get Random Attack
        // Setup Damage + Radius for this Animation
        // Launch Attack
        // Time the StopAll
        // Time the Damage
        // Reset StopAll
        if(!stopAnimation && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            stopAnimation = true;

            int rnd = Random.Range(0, animationAttack.Length);

            CalculateDamage(rnd);



            animator.Play(animationAttack[rnd]);
            Invoke(nameof(ResetBools), animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void ResetBools()
    {
        transform.position =  new Vector3(transform.localPosition.x, transform.localPosition.y + 0.4f, transform.localPosition.z);
        agent.SetDestination(new Vector3(DungeonMaster.Instance.player.transform.position.x, transform.position.y, DungeonMaster.Instance.player.transform.position.z));
        stopAnimation = false;
        stopAll = false;
    }

    public void StopAllAgent()
    {
        stopAll = true;
    }

    

    private void CalculateDamage(int rnd)
    {
        switch (rnd)
        {
            case 0:
                damagePosition = transform.position;
                damageRadius = 25;
                damage = 4;
                break;
            case 1:
                damagePosition = transform.position;
                damageRadius = 20;
                damage = 2;
                break;
            case 2:
                damagePosition = transform.position;
                damageRadius = 40;
                damage = 5;
                break;
        }
    }
}
