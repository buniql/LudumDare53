using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value;
    private GameObject Player;
    public float Speed;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collider Enter");
        if (col.tag == "Player")
        {
            GameObject.Find("Sound").GetComponent<Sound>().PlaySound(2);
            GameObject.Find("Player").GetComponent<PlayerController>().AddCoins(Value);
            Destroy(this.gameObject);
        }
        if (col.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
