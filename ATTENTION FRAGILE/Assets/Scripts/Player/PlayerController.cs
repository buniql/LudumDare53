using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats PlayerStats;

    private int currentHealth;
    public TextMeshProUGUI CurrentHealthUI;
    
    private int currentCoins = 0;
    public TextMeshProUGUI CurrentCointsUI;
    
    public GameObject Sprite;

    private float activeMovementSpeed;
    
    private bool canDash = true;

    private Camera _mainCamera;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        currentHealth = PlayerStats.Health;
        activeMovementSpeed = PlayerStats.MovementSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (dir.sqrMagnitude > 1f) dir.Normalize();

        _rigidbody2D.MovePosition(_rigidbody2D.position + dir * activeMovementSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        CurrentHealthUI.SetText(currentHealth.ToString());
        Mathf.Clamp(currentCoins, 0, 999);
        CurrentCointsUI.SetText(currentCoins.ToString());
        
        var direction = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (direction.x < transform.position.x)
            Sprite.transform.rotation = Quaternion.Euler(0, 180, 0);

        if (direction.x > transform.position.x)
            Sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //dash handling
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SetDashBools());
        }
    }
    private IEnumerator SetDashBools()
    {
        canDash = false;
        activeMovementSpeed = PlayerStats.DashSpeed;
        yield return new WaitForSeconds(PlayerStats.DashLength);
        activeMovementSpeed = PlayerStats.MovementSpeed;
        yield return new WaitForSeconds(PlayerStats.DashCooldown - PlayerStats.DashLength);
        canDash = true;
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
    }

    public void RemoveCoins(int amount)
    {
        currentCoins -= amount;
    }
}
