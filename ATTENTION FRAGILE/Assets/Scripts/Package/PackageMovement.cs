using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageMovement : MonoBehaviour
{
    public float ThrowSpeed = 30;
    private float currentTime = 0f;
    public float ThrowTime = 2f;

    private bool active = true;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < ThrowTime && active)
        {
            Vector2 up = (Vector2)transform.up;
            _rigidbody2D.MovePosition(_rigidbody2D.position + up * ThrowSpeed * Time.fixedDeltaTime);
            currentTime += Time.fixedDeltaTime;
        }

        if (currentTime >= ThrowTime) active = false;
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
        if (col.tag == "Player")
        {
            Debug.Log("If Statement");
            GameObject.Find("PackageShoot").GetComponent<PlayerThrow>().AddPackage();
            Destroy(gameObject);
        }
    }
}
