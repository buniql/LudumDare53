using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkEnemyController : EnemyController
{
    public override void DecreaseNeededPackageAmount()
    {
        NeededPackageAmount -= 1;
        transform.localScale *= 2;
    }
}
