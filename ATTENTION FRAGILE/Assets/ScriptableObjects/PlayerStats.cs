using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats Object", menuName = "Objects/Stats")]
public class PlayerStats : ScriptableObject
{
    public int Health;
    public float MovementSpeed;

    public float ProjectileRegenTime;
    public int MaxProjectiles;
    
    public float ShootSpeed;
    public float ShootDistance;
    public float ShootCooldown;

    public float DashSpeed;
    public float DashCooldown;
    public float DashLength;
}
