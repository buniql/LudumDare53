using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyController : EnemyController
{
    public float Force;
    public override IEnumerator Attack()
    {
        canAttack = false;
        Player.GetComponent<Rigidbody2D>().AddForce(direction * Force);
        Player.GetComponent<PlayerController>().DamagePlayer(AttackDamage);
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }
}
