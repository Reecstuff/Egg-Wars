using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !DungeonMaster.Instance.player.GravityOn)
        {
            transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.z), timeToKill * 2);
            Invoke(nameof(ResetPosition), timeToKill + 0.2f);
            Invoke(nameof(DamagePlayer), timeToKill);
        }
    }


    protected override void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !DungeonMaster.Instance.player.GravityOn)
        {
            CancelInvoke(nameof(DamagePlayer));
        }
    }

    protected override void DamagePlayer()
    {
        base.DamagePlayer();
    }

    void ResetPosition()
    {
        transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y - 1f, transform.localPosition.z), 0.2f);
    }
}
