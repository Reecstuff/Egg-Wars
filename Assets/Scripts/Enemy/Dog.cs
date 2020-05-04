using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dog : Enemy
{
    [SerializeField]
    AudioClip attackSound;

    [SerializeField]
    AudioClip deathSound;

    [SerializeField]
    AudioClip hitSound;


    public override void EnemyDying()
    {
        source.clip = deathSound;
        source.Play();
        animator.Play("Dog_Dying");
        Invoke(nameof(Die), 3.5f);
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
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Dog_Attack"))
        {
            transform.DOLookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), .2f);
            PlayAudio(attackSound);
            animator.Play("Dog_Attack");
            particleSystem.First().Play(); //Maybe Play
        }
        base.PlayerInTriggerStay(other);
    }

    public override void PlayerExitTrigger(GameObject other)
    {
            base.PlayerExitTrigger(other);
    }

}
