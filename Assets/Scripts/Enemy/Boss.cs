using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    string[] animationString = { "Boss_Standing", "Boss_Walking", "Boss_Jump", "Boss_Hack_Attack", "Boss_WirbelAttacke", "Boss_Dying" };
    string walkingSpeed = "Walking_Speed";

    protected override void OnPlayerCollision(GameObject Player)
    {
        base.OnPlayerCollision(Player);
    }


}
