using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    public GameObject EndScreen;
    public TextMeshProUGUI EndScreenUI;
    
    void Start()
    {
        Player = GameObject.Find("Player");

        EndScreen = Player.GetComponent<StatsHolder>().EndScreen;
        EndScreenUI = Player.GetComponent<StatsHolder>().EndScreenUI;
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        activeMovementspeed = MovementSpeed;
    }

    private void Update()
    {
        if(NeededPackageAmount <= 0) Die();
    }

    public override IEnumerator Attack()
    {
        canAttack = false;
        GameObject.Find("Sound").GetComponent<Sound>().PlaySound(4);
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

    public override void DecreaseNeededPackageAmount(int amount)
    {
        NeededPackageAmount -= amount;
        if (NeededPackageAmount % 6 == 0)
        {
            GameObject.Find("Sound").GetComponent<Sound>().PlaySound(3);
            
            Instantiate(PrefabOnDeath, Player.transform.position, Quaternion.identity);
        }
    }
    
    public virtual void Die()
    {
        GameObject.Find("Sound").GetComponent<Sound>().PlaySound(5);
        EndScreen.SetActive(true);
        EndScreenUI.SetText("you won!!");
        GameObject.Find("Player").GetComponent<StatsHolder>().RemoteRestart();
        Destroy(this.gameObject);
    }
}
