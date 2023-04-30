using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public StatsHolder StatsHolder;

    public GameObject Sprite;

    private bool canDash = true;

    private Camera _mainCamera;
    private Rigidbody2D _rigidbody2D;

    private bool dead = false;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (dir.sqrMagnitude > 1f) dir.Normalize();

        _rigidbody2D.MovePosition(_rigidbody2D.position + dir * StatsHolder.MovementSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (StatsHolder.Health <= 0)
        {
            dead = true;
            GameObject.Find("PlayerSprite").SetActive(false);
        }

        if (!dead)
        {
            var direction = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (direction.x < transform.position.x)
                Sprite.transform.rotation = Quaternion.Euler(0, 180, 0);

            if (direction.x > transform.position.x)
                Sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void DamagePlayer(int damage)
    {
        GameObject.Find("Sound").GetComponent<Sound>().PlaySound(1);
        StatsHolder.SetHealth(StatsHolder.Health - damage);
    }

    public void AddCoins(int amount)
    {
        StatsHolder.SetCoins(StatsHolder.Coins + amount);
    }
}
