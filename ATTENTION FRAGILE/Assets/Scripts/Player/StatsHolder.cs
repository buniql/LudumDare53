using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsHolder : MonoBehaviour
{
    public PlayerStats PlayerStats;
    
    [HideInInspector]
    public int Health;
    [HideInInspector]
    public float MovementSpeed;
    [HideInInspector]
    public int ProjectileDamage;
    [HideInInspector]
    public float ProjectileRegenTime;
    [HideInInspector]
    public int MaxProjectiles;
    [HideInInspector]
    public float ShootSpeed;
    [HideInInspector]
    public float ShootDistance;
    [HideInInspector]
    public float ShootCooldown;
    [HideInInspector]
    public int Coins;

    public TextMeshProUGUI CurrentHealthUI;
    public TextMeshProUGUI CurrentCointsUI;
    private void Start()
    {
        Mathf.Clamp(Coins, 0, 999);
        Health = PlayerStats.Health;
        MovementSpeed = PlayerStats.MovementSpeed;
        ProjectileRegenTime = PlayerStats.ProjectileRegenTime;
        MaxProjectiles = PlayerStats.MaxProjectiles;
        ShootSpeed = PlayerStats.ShootSpeed;
        ShootDistance = PlayerStats.ShootDistance;
        ShootCooldown = PlayerStats.ShootCooldown;
    }

    private void Update()
    {
        CurrentHealthUI.SetText(Health.ToString());
        CurrentCointsUI.SetText(Coins.ToString());

        if (Health <= 0)
        {
            Destroy(this.gameObject);
            //TODO Death Sound
        }
    }

    public void SetHealth(int value)
    {
        Health = value;
    }
    public void SetMovementSpeed(float value)
    {
        MovementSpeed = value;
    }

    public void SetProjectileDamage(int value)
    {
        ProjectileDamage = value;
    }
    public void SetProjectileRegenTime(float value)
    {
        ProjectileRegenTime = value;
    }
    public void SetMaxProjectiles(int value)
    {
        MaxProjectiles = value;
    }
    public void SetShootSpeed(float value)
    {
        ShootSpeed = value;
    }
    public void SetShootDistance(float value)
    {
        ShootDistance = value;
    }
    public void SetShootCooldown(float value)
    {
        ShootCooldown = value;
    }
    public void SetCoins(int value)
    {
        Coins = value;
    }
}
