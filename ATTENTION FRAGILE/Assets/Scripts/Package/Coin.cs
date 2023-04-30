using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collider Enter");
        if (col.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerController>().AddCoins(1);
            Destroy(this.gameObject);
        }
    }
}
