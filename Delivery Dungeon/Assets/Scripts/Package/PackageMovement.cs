using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageMovement : MonoBehaviour
{
    public StatsHolder StatsHolder;
    
    private PlayerThrow playerThrow;
    private float currentTime = 0f;

    private Vector2 direction = Vector2.zero;
    public bool active = true;
    
    private Camera _mainCamera;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        StatsHolder = GameObject.Find("Player").GetComponent<StatsHolder>();

        _mainCamera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerThrow = GameObject.Find("Player").GetComponent<PlayerThrow>();
        
        direction = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        direction = direction.normalized;
        
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(SetColliderActive());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentTime < StatsHolder.ShootDistance && active)
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + direction * StatsHolder.ShootSpeed * Time.fixedDeltaTime);
            currentTime += Time.fixedDeltaTime;
        }

        if (currentTime >= StatsHolder.ShootDistance)
        {
            active = false;
        }
    }

    private IEnumerator SetColliderActive()
    {
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    public void Reset()
    {
        currentTime = 0f;
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collider Enter");
        if (col.tag == "Player" && playerThrow.PackageAmount <= StatsHolder.MaxProjectiles)
        {
            GameObject.Find("Player").GetComponent<PlayerThrow>().AddPackage();
            Destroy(this.gameObject);
        }

        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyController>().DecreaseNeededPackageAmount(1);
            Destroy(this.gameObject);
        }
        
        if (col.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
