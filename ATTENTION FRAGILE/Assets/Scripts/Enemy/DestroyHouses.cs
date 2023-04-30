using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHouses : EnemyController
{
    public override void Behaviour()
    {
        if (NeededPackageAmount <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        GameObject.Find("Sound").GetComponent<Sound>().PlaySound(5);
        Destroy(this.gameObject);
    }
}
