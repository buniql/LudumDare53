using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public Transform Player;

    public float AttackCooldown = 1f;
    private bool canAttack = true;
    
    public GameObject PacketPrefab;
    private List<GameObject> PacketList = new List<GameObject>();

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position + Quaternion.Euler(0, 0, AngleBetweenTwoPoints((Vector2)transform.position, _mainCamera.ScreenToWorldPoint(Input.mousePosition)) + 90f) * Vector3.up;
        
        if (Input.GetMouseButton(0) &&  canAttack && PacketList.Count != 0)
        {
            StartCoroutine(ThrowPackage());
        }
    }

    private IEnumerator ThrowPackage()
    {
        canAttack = false;
        transform.rotation = Quaternion.Euler(0, 0, AngleBetweenTwoPoints((Vector2)transform.position, _mainCamera.ScreenToWorldPoint(Input.mousePosition)) + 90f); 
        Instantiate(PacketList[0], transform.position, transform.rotation);
        PacketList.RemoveAt(0);
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }
    
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void AddPackage()
    {
        PacketList.Add(PacketPrefab);
    }
}
