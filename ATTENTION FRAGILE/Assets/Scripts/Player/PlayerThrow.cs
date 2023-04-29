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
    private List<GameObject> PacketList = new List<GameObject>();

    private Camera _mainCamera;

    public TextMeshProUGUI PackageAmountUI;
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        PackageAmountUI.SetText(PacketList.Count.ToString());
        if (Input.GetMouseButton(0) &&  canAttack && PacketList.Count != 0)
        {
            StartCoroutine(ThrowPackage());
        }
    }

    private IEnumerator ThrowPackage()
    {
        canAttack = false;
        Instantiate(PacketList[0], transform.position, Quaternion.identity);
        PacketList.RemoveAt(0);
        yield return new WaitForSeconds(PlayerStats.ShootCooldown);
        canAttack = true;
    }

    public void AddPackage()
    {
        PacketList.Add(PacketPrefab);
    }
}
