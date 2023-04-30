using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collider Enter");
        if (col.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerController>().AddCoins(Value);
            Destroy(this.gameObject);
        }
        if (col.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
