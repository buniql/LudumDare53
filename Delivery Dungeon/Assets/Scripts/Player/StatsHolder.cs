using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsHolder : MonoBehaviour
{
    public PlayerStats PlayerStats;
    
    public int Health;
    public float MovementSpeed;
    public float ProjectileSize;
    public float ProjectileRegenTime;
    public int MaxProjectiles;
    public float ShootSpeed;
    public float ShootDistance;
    public float ShootCooldown;
    public int Coins;

    public TextMeshProUGUI CurrentHealthUI;
    public TextMeshProUGUI CurrentCointsUI;

    public GameObject EndScreen;
    public TextMeshProUGUI EndScreenUI;
    private void Start()
    {
        Health = PlayerStats.Health;
        MovementSpeed = PlayerStats.MovementSpeed;
        ProjectileSize = PlayerStats.ProjectileSize;
        ProjectileRegenTime = PlayerStats.ProjectileRegenTime;
        MaxProjectiles = PlayerStats.MaxProjectiles;
        ShootSpeed = PlayerStats.ShootSpeed;
        ShootDistance = PlayerStats.ShootDistance;
        ShootCooldown = PlayerStats.ShootCooldown;
    }

    private void Update()
    {
        Mathf.Clamp(MovementSpeed, 0, 30);
        Mathf.Clamp(ProjectileSize, 1f, 5f);
        Mathf.Clamp(ProjectileRegenTime, 0.1f, 10f);
        Mathf.Clamp(ShootSpeed, 20, 80);
        Mathf.Clamp(ShootCooldown, 0.1f, 10f);
        Mathf.Clamp(Coins, 0, 999);
        
        CurrentHealthUI.SetText(Health.ToString());
        CurrentCointsUI.SetText(Coins.ToString());

        if (Health <= 0)
        {
            EndScreen.SetActive(true);
            EndScreenUI.SetText("you died..");
            StartCoroutine(RestartGame());
        }
    }

    public void RemoteRestart()
    {
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Game");
    }

    public void SetHealth(int value)
    {
        Health = value;
    }
    public void SetMovementSpeed(float value)
    {
        MovementSpeed = value;
    }
    public void SetProjectileSize(float value)
    {
        ProjectileSize = value;
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
