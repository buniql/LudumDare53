using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{
    public GameObject parent;

    public GameObject UItoShow;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            foreach (Transform child in parent.transform) {
                GameObject.Destroy(child.gameObject);
            }

            GameObject element = Instantiate(UItoShow, Vector3.zero, Quaternion.identity);
            element.transform.SetParent(parent.transform, false);
        }
    }
}
