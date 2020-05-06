using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dog : Enemy
{
    [SerializeField]
    protected AudioClip attackSound;

    [SerializeField]
    protected AudioClip deathSound;

    [SerializeField]
    protected AudioClip hitSound;

    [SerializeField]
    protected string DeathAnimationString;

    [SerializeField]
    protected string AttackString;



    public override void EnemyDying()
    {
        base.EnemyDying();
        particleSystem.First().Stop();
        isdying = true;
        if(!source.clip.Equals(deathSound))
        {
            source.clip = deathSound;
            source.Play();
        }
        animator.Play(DeathAnimationString);
        Invoke(nameof(Die), 3.5f);
    }

    public override void GotHit(int damage)
    {
        base.GotHit(damage);
        if (!isdying && hitSound && (!source.isPlaying || source.isPlaying && source.clip.Equals(attackSound)))
        {
            source.clip = hitSound;
            source.Play();
        }
    }

    public override void DamagePlayer()
    {
        base.DamagePlayer();
        DungeonMaster.Instance.player.characterStats.TakeDamage(damage);
    }

    public override void PlayerEnterTrigger(GameObject other)
    {
        base.PlayerEnterTrigger(other);
    }

    public override void PlayerInTriggerStay(GameObject other)
    {
        if(!isdying)
        {
            transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName(AttackString))
            {
                PlayAudio(attackSound);
                animator.Play(AttackString);
                particleSystem.First().Play(); //Maybe Play
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName(AttackString))
            {
                 //Maybe Play
            }
        }
        base.PlayerInTriggerStay(other);
    }

    public override void PlayerExitTrigger(GameObject other)
    {
            base.PlayerExitTrigger(other);
    }

}
