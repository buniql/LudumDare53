using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float ShootSpeed;
    public int ProjectileDamage;

    private Rigidbody2D _rigidbody2D;
    private Vector2 direction;

    private void Start()
    {
        direction =  GameObject.Find("Player").transform.position - transform.position;
        direction = direction.normalized;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + direction * ShootSpeed * Time.fixedDeltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collider Enter");
        if (col.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerController>().DamagePlayer(ProjectileDamage);
            Destroy(this.gameObject);
        }
        if (col.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
