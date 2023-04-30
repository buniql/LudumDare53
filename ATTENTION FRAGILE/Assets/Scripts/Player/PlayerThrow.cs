using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public PlayerStats PlayerStats;
    
    private bool canAttack = true;
    
    public GameObject PacketPrefab;
    public int PackageAmount = 0;

    private bool PackageRegen = true;

    private Camera _mainCamera;

    public TextMeshProUGUI PackageAmountUI;
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        PackageAmountUI.SetText(PackageAmount.ToString());
        if (Input.GetMouseButton(0) &&  canAttack && PackageAmount != 0)
        {
            StartCoroutine(ThrowPackage());
        }

        if (PackageRegen && PackageAmount <= PlayerStats.MaxProjectiles) StartCoroutine(RegenPackage());
    }

    private IEnumerator RegenPackage()
    {
        PackageRegen = false;
        AddPackage();
        yield return new WaitForSeconds(PlayerStats.ProjectileRegenTime);
        PackageRegen = true;
    }

    private IEnumerator ThrowPackage()
    {
        canAttack = false;
        Instantiate(PacketPrefab, transform.position, Quaternion.identity);
        PackageAmount -= 1;
        yield return new WaitForSeconds(PlayerStats.ShootCooldown);
        canAttack = true;
    }

    public void AddPackage()
    {
        PackageAmount += 1;
    }
}
