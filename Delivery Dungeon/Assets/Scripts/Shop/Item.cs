using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Object", menuName = "Objects/Item")]
public class Item : ScriptableObject
{
    public string Name;
    
    public int Price;
    
    public int Health;
    public float MovementSpeed;
    
    public float ProjectileSize;
    public float ProjectileRegenTime;
    public int MaxProjectiles;
    
    public float ShootSpeed;
    public float ShootDistance;
    public float ShootCooldown;
}
