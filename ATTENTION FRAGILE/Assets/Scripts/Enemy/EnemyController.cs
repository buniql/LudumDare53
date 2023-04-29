using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float NeededPackageAmount;
    public float MovementSpeed;
    public float ActivationDistance;
    public int AttackDamage;
    public float AttackRange;
    public float AttackCooldown;
    private bool canAttack = true;
    private GameObject Player;
    public GameObject PrefabOnDeath;
    private Vector3 direction;
    
    private bool activated = false;
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        Player = GameObject.Find("Player");
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        direction = Player.transform.position - transform.position;

        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (direction.sqrMagnitude > 1f) direction.Normalize();

        if (Vector3.Distance(transform.position, Player.transform.position) < ActivationDistance) activated = true;

        if (activated)
        {
            Move();
        }

        if (NeededPackageAmount == 0)
        {
            var spawn = Instantiate(PrefabOnDeath, transform.position, Quaternion.identity);
            spawn.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
        
        if(Vector3.Distance(transform.position, Player.transform.position) < AttackRange && canAttack)
        {
            StartCoroutine((Attack()));
        }
    }

    private void Move()
    {
        _rigidbody2D.MovePosition(transform.position + direction * MovementSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        Player.GetComponent<PlayerController>().DamagePlayer(AttackDamage);
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }

    public void DecreaseNeededPackageAmount()
    {
        NeededPackageAmount -= 1;
    }
}
