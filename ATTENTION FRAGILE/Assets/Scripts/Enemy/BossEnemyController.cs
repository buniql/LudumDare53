using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyController
{
    public GameObject Projectile;

    private float activeMovementspeed;
    
    public float TeleportSpeed;
    
    public float DistanceToTeleport;

    public float TeleportTime;

    public float TeleportCooldown;

    private bool canTeleport = true;
    
    void Start()
    {
        Player = GameObject.Find("Player");
        _rigidbody2D = GetComponent<Rigidbody2D>();
        activeMovementspeed = MovementSpeed;
    }

    public override IEnumerator Attack()
    {
        canAttack = false;
        Instantiate(Projectile, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }

    public override void Move()
    {
        _rigidbody2D.MovePosition(transform.position + direction * activeMovementspeed * Time.fixedDeltaTime);
        
        if (Vector2.Distance(transform.position, Player.transform.position) > DistanceToTeleport && canTeleport)
        {
            StartCoroutine(WaitForTeleportCooldown());
        }
    }

    private IEnumerator WaitForTeleportCooldown()
    {
        canTeleport = false;
        activeMovementspeed = TeleportSpeed;
        yield return new WaitForSeconds(TeleportTime);
        activeMovementspeed = MovementSpeed;
        yield return new WaitForSeconds(TeleportCooldown);
        canTeleport = true;
    }

    public override void DecreaseNeededPackageAmount()
    {
        NeededPackageAmount -= 1;
        if (NeededPackageAmount % 5 == 0)
        {
            Instantiate(PrefabOnDeath, Player.transform.position, Quaternion.identity);
        }
    }
    
    public virtual void Die()
    {
        Destroy(gameObject);
        //EndScreen + Sound!
    }
}
