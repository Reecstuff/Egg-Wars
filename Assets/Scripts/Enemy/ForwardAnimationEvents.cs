using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardAnimationEvents : MonoBehaviour
{

    Boss thisBoss;
    private void Start()
    {
        thisBoss = GetComponentInParent<Boss>();
    }

    public void StopAllAnimation()
    {
        thisBoss.StopAllAgent();
    }

    public void DamagePlayer()
    {
        thisBoss.DamagePlayer();
    }
}
