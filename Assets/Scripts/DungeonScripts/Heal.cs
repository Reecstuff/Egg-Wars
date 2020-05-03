using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : EquipAbleItem
{
    [SerializeField]
    int healAmount = 1;

    [SerializeField]
    ParticleSystem[] particleSystem;

    public void HealPlayer()
    {
        DungeonMaster.Instance.player.characterStats.Heal(healAmount);
        transform.DOScale(0, 1.8f);
        for (int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].Play();
        }


        Invoke(nameof(DestroyThis), 3f);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
