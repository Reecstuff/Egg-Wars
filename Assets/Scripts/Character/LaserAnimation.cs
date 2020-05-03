using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimation : MonoBehaviour
{
    [SerializeField]
    Transform Laser;

    Transform startTransform;

    private void Start()
    {
        Laser.gameObject.SetActive(false);
    }

    public void ShowLaser()
    {
        Laser.gameObject.SetActive(true);
        CancelInvoke(nameof(HideLaser));
        Invoke(nameof(HideLaser), 3f);
    }

    void HideLaser()
    {
        Laser.gameObject.SetActive(false);
    }

}
