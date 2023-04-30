using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public StatsHolder StatsHolder;
    
    private bool canAttack = true;
    
    public GameObject PacketPrefab;
    public int PackageAmount = 0;

    private bool PackageRegen = true;

    private Camera _mainCamera;

    public TextMeshProUGUI PackageAmountUI;

    public GameObject ShopUI;

    private bool dead = false;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (StatsHolder.Health <= 0)
        {
            dead = true;
        }

        if (!dead)
        {
            PackageAmountUI.SetText(PackageAmount.ToString());

            if (Input.GetMouseButton(0) &&  canAttack && PackageAmount != 0 && !ShopUI.active)
            {
                StartCoroutine(ThrowPackage());
            }

            if (PackageRegen && PackageAmount <= StatsHolder.MaxProjectiles) StartCoroutine(RegenPackage());
        }

    }

    private IEnumerator RegenPackage()
    {
        PackageRegen = false;
        AddPackage();
        yield return new WaitForSeconds(StatsHolder.ProjectileRegenTime);
        PackageRegen = true;
    }

    private IEnumerator ThrowPackage()
    {
        canAttack = false;
        GameObject.Find("Sound").GetComponent<Sound>().PlaySound(0);
        GameObject package = Instantiate(PacketPrefab, transform.position, Quaternion.identity);
        package.transform.localScale = new Vector3(StatsHolder.ProjectileSize, StatsHolder.ProjectileSize, 1);
        
        PackageAmount -= 1;
        yield return new WaitForSeconds(StatsHolder.ShootCooldown);
        canAttack = true;
    }

    public void AddPackage()
    {
        PackageAmount += 1;
    }
}
