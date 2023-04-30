using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkEnemyController : EnemyController
{
    public override void DecreaseNeededPackageAmount(int amount)
    {
        NeededPackageAmount -= amount;
        transform.localScale *= 2;
    }
}
