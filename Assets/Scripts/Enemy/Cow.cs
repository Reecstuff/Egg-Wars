using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Enemy
{
    protected override void OnPlayerCollision()
    {
        base.OnPlayerCollision();
        transform.DOScale(1.5f, 0.6f);
        maxPitch = 0;
        source.Stop();
        source.Play();
        sonar.gameObject.SetActive(false);
        source.DOPitch(3, 0.1f);
        source.DOFade(2, 0.5f);
        Invoke(nameof(PlayParticles), 0.5f);
        Invoke(nameof(Die), 1f);
    }
}
