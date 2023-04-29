using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Sprite;

    public float MovementSpeed = 5;
    
    private float activeMovementSpeed;
    
    public float DashSpeed = 10;
    public float DashDuration = .5f;
    public float DashCooldown = 2;
    private bool canDash = true;

    private Camera _mainCamera;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        activeMovementSpeed = MovementSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (dir.sqrMagnitude > 1f) dir.Normalize();

        _rigidbody2D.MovePosition(_rigidbody2D.position + dir * activeMovementSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
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
        activeMovementSpeed = DashSpeed;
        yield return new WaitForSeconds(DashDuration);
        activeMovementSpeed = MovementSpeed;
        yield return new WaitForSeconds(DashCooldown - DashDuration);
        canDash = true;
    }
}