using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cat : Enemy
{
    [SerializeField]
    AudioClip attackSound;

    [SerializeField]
    AudioClip deathSound;

    [SerializeField]
    AudioClip hitSound;

    bool isdying = false;


    public override void EnemyDying()
    {
        particleSystem.First().Stop();
        isdying = true;
        source.clip = deathSound;
        source.Play();
        animator.Play("Cat_Dying");
        Invoke(nameof(Die), 4f);
    }

    public override void GotHit(int damage)
    {
        base.GotHit(damage);
        if (hitSound)
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
        if(!isdying && !animator.GetCurrentAnimatorStateInfo(0).IsName("Cat_Shooting"))
        {
            transform.DOLookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), .2f);
            animator.Play("Cat_Shooting");
            particleSystem.First().Play();
            PlayAudio(attackSound);
        }
        base.PlayerInTriggerStay(other);
    }

    public override void PlayerExitTrigger(GameObject other)
    {
            base.PlayerExitTrigger(other);
    }

}
